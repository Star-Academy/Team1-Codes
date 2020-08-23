using System;
using SearchNest.Model;

namespace SearchNest
{
    class Program
    {
        private const string DocumentsPath = @"./../../../Resources/documents";
        private const string IndexName = "phase8_1";

        static void Main(string[] args)
        {
            var client = ElasticClientManager.GetElasticClient();

            var allFilesReader = new DataReader.AllFilesReader(DocumentsPath);
            var documents = new DocumentBuilder().BuildDocuments(allFilesReader.ReadAllFiles());
            var importer = new Importer<Document>();
            importer.ImportData(IndexName, documents);
            client.Indices.Refresh(); // ?

            var queryHandler = new QueryHandler();
            while (true) {
                Console.WriteLine(queryHandler.handleQuery(Console.ReadLine(), IndexName));
            }
        }
    }
}
