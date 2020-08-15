using System.Collections.Generic;
using SearchLibrary.ReadFiles;

namespace SearchLibrary
{
    public class QueryProcessor
    {
        private bool seenAndQuery, seenOrQuery;
        private readonly InvertedIndex invertedIndex;

        public QueryProcessor(InvertedIndex invertedIndex)
        {
            this.invertedIndex = invertedIndex;
        }

        public HashSet<string> Process(string rawQuery)
        {
            seenAndQuery = false;
            seenOrQuery = false;

            var query = Analyze(rawQuery);
            var foundDoc = SearchQueries(query);
            return BuildResult(foundDoc);
        }
        
        public Query Analyze(string rawQuery)
        {
            var result = new Query();
            var andQueries = new List<string>();
            var orQueries = new List<string>();
            var excQueries = new List<string>();

            var words = rawQuery.Split(' ');
            foreach (var word in words)
            {
                switch (word[0])
                {
                    case '+':
                        seenOrQuery = true;
                        orQueries.Add(word.Substring(1));
                        break;
                    case '-':
                        excQueries.Add(word.Substring(1));
                        break;
                    default:
                        seenAndQuery = true;
                        andQueries.Add(word);
                        break;
                }
            }
            result.OrQueries = orQueries;
            result.AndQueries = andQueries;
            result.ExcQueries = excQueries;
            return result;
        }

        public Doc SearchQueries(Query query)
        {
            var resultDocs = new Doc();
            var temp = new HashSet<string>();
            foreach (var word in query.AndQueries)
                if (invertedIndex.Map.TryGetValue(word, out temp))
                    resultDocs.AndDocs.UnionWith(temp);

            foreach (var word in query.OrQueries)
                if (invertedIndex.Map.TryGetValue(word, out temp))
                    resultDocs.OrDocs.UnionWith(temp);

            foreach (var word in query.ExcQueries)
                if (invertedIndex.Map.TryGetValue(word, out temp))
                    resultDocs.ExcDocs.UnionWith(temp);

            if (seenAndQuery && resultDocs.AndDocs.Count == 0)
                resultDocs.IncompatibleAnds = true;

            if (seenOrQuery && resultDocs.OrDocs.Count == 0)
                resultDocs.IncompatibleOrs = true;

            return resultDocs;
        }

        public HashSet<string> BuildResult(Doc foundDocs)
        {
            HashSet<string> result;
            if (foundDocs.IncompatibleAnds || foundDocs.IncompatibleOrs)
                result = new HashSet<string>();
            else if (!seenAndQuery && !seenOrQuery)
                result = invertedIndex.AllFilesNames;
            else if (!seenAndQuery)
                result = foundDocs.OrDocs;
            else if (!seenOrQuery)
                result = foundDocs.AndDocs;
            else
            {
                result = foundDocs.AndDocs;
                result.IntersectWith(foundDocs.OrDocs);
            }
            result.ExceptWith(foundDocs.ExcDocs);

            return result;
        }
    }
}