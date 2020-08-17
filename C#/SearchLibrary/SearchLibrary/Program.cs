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
        private QueryProcessor queryProcessor;
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
            return GetQueryResultOutput(result);
        }

        private static string GetQueryResultOutput(ICollection<string> result)
        {
            return result.Any()
                ? $"Search results: {result.Aggregate((x, y) => $"{x}, {y}")}"
                : "Query wasn't found";
        }
    }
}