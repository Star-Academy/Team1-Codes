using System;
using SearchLibrary;

namespace SearchConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string DocumentsPath = "../../Resources/documents";

            var searchProgram = new SearchProgram();
            searchProgram.SetUpInvertIndexSearch(DocumentsPath);

            while (true) {
                Console.WriteLine(searchProgram.Search(Console.ReadLine()));
            }
        }
    }
}
