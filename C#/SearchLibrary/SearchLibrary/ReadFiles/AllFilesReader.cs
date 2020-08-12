using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SearchLibrary.ReadFiles
{
    public class AllFilesReader
    {
        private string documentsPath;

        public AllFilesReader(string folderPath)
        {
            documentsPath = folderPath;
        }

        public Dictionary<string, List<string>> ReadAllFiles()
        {
            var allFilesContents = new Dictionary<string, List<string>>();

            var fileReader = new FileReader();
            var tokenizer = new Tokenizer("\\w+");
            string[] filePaths = Directory.GetFiles(documentsPath, "*.txt", SearchOption.AllDirectories);
            foreach (var filePath in filePaths)
            {
                var contents = fileReader.GetContent(filePath);
                var tokenizedContent = tokenizer.getTokens(contents);
                allFilesContents.Add(Path.GetFileName(filePath), tokenizedContent);
            }
            return allFilesContents;
        }

        public List<string> getAllFilesNames()
        {
            return Directory.GetFiles(documentsPath, "*.txt", SearchOption.AllDirectories)
                .Select(x => Path.GetFileName(x)).ToList();
        }
    }
}