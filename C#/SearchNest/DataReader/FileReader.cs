using System.IO;

namespace SearchNest.DataReader
{
    public class FileReader : IReader
    {
        public string GetContent(string path)
        {
            return File.ReadAllText(path);
        }
    }
}