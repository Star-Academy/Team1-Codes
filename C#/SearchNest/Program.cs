using System;
using SearchNest.Model;

namespace SearchNest
{
    class Program
    {
        private const string DocumentsPath = @"./../../Resources/documents";
        private const string IndexName = "phase8";

        static void Main(string[] args)
        {
            var allFilesReader = new DataReader.AllFilesReader(DocumentsPath);
            var documents = new DocumentBuilder().BuildDocuments(allFilesReader.ReadAllFiles());
            var importer = new Importer<Document>();
            importer.importData(IndexName, documents);
        }
    }
}
