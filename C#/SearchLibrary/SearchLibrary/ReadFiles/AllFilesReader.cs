using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SearchLibrary.ReadFiles
{
    public class AllFilesReader
    {
        FileReader fileReader = new FileReader();
        private string documentsPath;

        public AllFilesReader(string folderPath)
        {
            documentsPath = folderPath;
        }

        public Dictionary<string, List<string>> ReadAllFiles()
        {
            var allFilesContents = new Dictionary<string, List<string>>();

            var tokenizer = new Tokenizer("\\w+");
            string[] filePaths = Directory.GetFiles(documentsPath, "*.txt", SearchOption.AllDirectories);
            foreach (var filePath in filePaths)
            {
                allFilesContents.Add(Path.GetFileName(filePath), ReadFile(filePath, tokenizer));
            }
            return allFilesContents;
        }

        public List<string> ReadFile(string filePath, Tokenizer tokenizer) {
            return tokenizer.getTokens(fileReader.GetContent(filePath));
        }

        public List<string> getAllFilesNames()
        {
            return Directory.GetFiles(documentsPath, "*.txt", SearchOption.AllDirectories)
                .Select(x => Path.GetFileName(x)).ToList();
        }
    }
}