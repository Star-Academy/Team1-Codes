using System.Collections.Generic;
using System.IO;

namespace SearchLibrary.ReadFiles
{
    public class AllFilesReader
    {
        private string documentsPath;

        public AllFilesReader(string folderPath)
        {
            documentsPath = folderPath;
        }

        Dictionary<string, List<string>> ReadAllFiles()
        {
            var allFilesContents = new Dictionary<string, List<string>>();
            
            var fileReader = new FileReader();
            string[] filePaths = Directory.GetFiles(documentsPath, "*.txt", SearchOption.AllDirectories);
            foreach (var filePath in filePaths)
            {
                var contents = fileReader.GetContent(filePath);
                allFilesContents.Add(Path.GetFileName(filePath), contents);
            }
            return allFilesContents;
        }
    }
}