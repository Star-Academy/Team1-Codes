using System;
using System.IO;
using SearchLibrary.ReadFiles;

namespace SearchLibrary
{
    public class Program
    {
        public void Main(string[] args){
            const string DocumentsPath = "..\\..\\..\\..\\..\\..\\Resources\\documents";
            var readFiles = new AllFilesReader(DocumentsPath).ReadAllFiles();
            InvertedIndex inverted = new InvertedIndex();
            inverted.InitMap(readFiles);
        }
    }
}