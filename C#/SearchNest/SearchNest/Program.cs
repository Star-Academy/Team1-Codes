using System;
using Nest;
using SearchNest.Model;

namespace SearchNest
{
    internal static class Program
    {
        private const string DocumentsPath = @"./../../../Resources/documents";
        private const string IndexName = "phase08";
        private static readonly IElasticClient Client = ElasticClientManager.GetElasticClient();

        private static void Main(string[] args)
        {
            // SetUpIndex();
            ProcessQueries();
        }

        private static void ProcessQueries()
        {
            var queryHandler = new QueryHandler(Client, IndexName);
            while (true)
                Console.WriteLine(queryHandler.HandleQuery(Console.ReadLine()));
        }

        private static void SetUpIndex()
        {
            var allFilesReader = new DataReader.AllFilesReader(DocumentsPath);
            var documents = DocumentBuilder.BuildDocuments(allFilesReader.ReadAllFiles());
            var importer = new Importer<Document>();
            importer.ImportData(IndexName, documents);

            Client.Indices.Refresh();
        }
    }
}
