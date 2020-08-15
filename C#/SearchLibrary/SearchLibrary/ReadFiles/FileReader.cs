using System.IO;

namespace SearchLibrary.ReadFiles
{
    public class FileReader : IReader
    {
        public string GetContent(string path)
        {
            return File.ReadAllText(path).ToLower();
        }
    }
}