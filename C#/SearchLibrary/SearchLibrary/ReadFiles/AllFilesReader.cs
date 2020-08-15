using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SearchLibrary.ReadFiles
{
    public class AllFilesReader
    {
        private readonly FileReader fileReader = new FileReader();
        private readonly string documentsPath;

        public AllFilesReader(string folderPath)
        {
            documentsPath = folderPath;
        }

        public Dictionary<string, List<string>> ReadAllFiles()
        {
            var tokenizer = new Tokenizer("\\w+");
            var filePaths = Directory.GetFiles(documentsPath, "*.txt", SearchOption.AllDirectories);
            return filePaths.ToDictionary(Path.GetFileName,
                filePath => ReadFile(filePath, tokenizer));
        }

        public List<string> ReadFile(string filePath, Tokenizer tokenizer)
        {
            return tokenizer.GetTokens(fileReader.GetContent(filePath));
        }

        public List<string> GetAllFilesNames()
        {
            return Directory.GetFiles(documentsPath, "*.txt", SearchOption.AllDirectories)
                .Select(Path.GetFileName).ToList();
        }
    }
}