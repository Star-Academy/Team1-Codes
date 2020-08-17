using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SearchLibrary.ReadFiles
{
    public class AllFilesReader
    {
        private readonly FileReader fileReader = new FileReader();
        private readonly string documentsPath;

        const string SearchPattern = "*.txt";
        const string SearchRegex = "\\w+";

        public AllFilesReader(string folderPath)
        {
            documentsPath = folderPath;
        }

        public Dictionary<string, List<string>> ReadAllFiles()
        {
            var tokenizer = new Tokenizer(SearchRegex);
            var filePaths = Directory.GetFiles(documentsPath, SearchPattern, SearchOption.AllDirectories);
            return filePaths.ToDictionary(Path.GetFileName,
                filePath => ReadFile(filePath, tokenizer));
        }

        public List<string> ReadFile(string filePath, Tokenizer tokenizer)
        {
            return tokenizer.GetTokens(fileReader.GetContent(filePath));
        }

        public List<string> GetAllFilesNames()
        {
            return Directory.GetFiles(documentsPath, SearchPattern, SearchOption.AllDirectories)
                .Select(Path.GetFileName).ToList();
        }
    }
}