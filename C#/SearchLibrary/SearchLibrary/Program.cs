using SearchLibrary.ReadFiles;
using System.Collections.Generic;
using System.Linq;

namespace SearchLibrary
{
    /// <summary>
    /// By calling SetUpInvertIndexSearch(documentsPath), Search program is initialized
    /// and then you can start calling Search(query) to get results
    /// </summary>
    public class Program
    {
        QueryProcessor queryProcessor;
        public void SetUpInvertIndexSearch(string documentsPath)
        {
            var filesContents = new AllFilesReader(documentsPath).ReadAllFiles();
            var invertedIndex = new InvertedIndex();
            invertedIndex.InitMap(filesContents);
            queryProcessor = new QueryProcessor(invertedIndex);
        }

        public string Search(string query)
        {
            var result = queryProcessor.Process(query);
            return getQueryResultOutput(result);
        }

        public string getQueryResultOutput(HashSet<string> result)
        {
            if (result.Count != 0)
                return $"Search results: {result.Aggregate((x, y) => $"{x}, {y}")}";
            return "Query wasn't found";
        }
    }
}