using System.Collections.Generic;
using System.Linq;

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
            var words = rawQuery.Split(' ');

            var andQueries = words.Where(w => w[0] != '-' && w[0] != '+').ToList();
            var orQueries = GetQueries(words, '+');
            var excQueries = GetQueries(words, '-');

            seenOrQuery = orQueries.Any();
            seenAndQuery = andQueries.Any();
            
            result.OrQueries = orQueries;
            result.AndQueries = andQueries;
            result.ExcQueries = excQueries;
            return result;
        }

        private List<string> GetQueries(string[] words, char startChar) {
            return words.Where(w => w[0] == startChar).Select(w => w.Substring(1)).ToList();
        }

        public Doc SearchQueries(Query query)
        {
            var resultDocs = new Doc();

            resultDocs.AndDocs = GetAvailableDocs(query.AndQueries);
            resultDocs.OrDocs = GetAvailableDocs(query.OrQueries);
            resultDocs.ExcDocs = GetAvailableDocs(query.ExcQueries);

            if (seenAndQuery && !resultDocs.AndDocs.Any())
                resultDocs.IncompatibleAnds = true;

            if (seenOrQuery && !resultDocs.OrDocs.Any())
                resultDocs.IncompatibleOrs = true;

            return resultDocs;
        }

        private HashSet<string> GetAvailableDocs(List<string> queries) {
            var result = new HashSet<string>();
            foreach (var word in queries)
                if (invertedIndex.Map.TryGetValue(word, out var temp))
                    result.UnionWith(temp);
            return result;
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