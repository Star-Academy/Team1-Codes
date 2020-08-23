using System;
using SearchNest.Model;

namespace SearchNest
{
    internal static class Program
    {
        private const string DocumentsPath = @"./../../../Resources/documents";
        private const string IndexName = "phase08";

        private static void Main(string[] args)
        {
            SetUpIndex();
            ProcessQueries();
        }

        private static void ProcessQueries()
        {
            var queryHandler = new QueryHandler();
            while (true)
                Console.WriteLine(queryHandler.HandleQuery(Console.ReadLine(), IndexName));
        }

        private static void SetUpIndex()
        {
            var allFilesReader = new DataReader.AllFilesReader(DocumentsPath);
            var documents = DocumentBuilder.BuildDocuments(allFilesReader.ReadAllFiles());
            var importer = new Importer<Document>();
            importer.ImportData(IndexName, documents);
            
            var client = ElasticClientManager.GetElasticClient();
            client.Indices.Refresh();
        }
    }
}
