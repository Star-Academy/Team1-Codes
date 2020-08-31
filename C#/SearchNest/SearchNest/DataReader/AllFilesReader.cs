using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SearchNest.DataReader
{
    public class AllFilesReader
    {
        private readonly FileReader fileReader = new FileReader();
        private readonly string documentsPath;

        private const string SearchPattern = "*";

        public AllFilesReader(string folderPath)
        {
            documentsPath = folderPath;
        }

        public Dictionary<string, string> ReadAllFiles()
        {
            var filePaths = Directory.GetFiles(documentsPath, SearchPattern, SearchOption.AllDirectories);
            return filePaths.ToDictionary(Path.GetFileName,
                filePath => fileReader.GetContent(filePath));
        }
    }
}