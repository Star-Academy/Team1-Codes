using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchLibrary.ReadFiles
{
    public class FileReader : IReader
    {
        public string GetContent(string path)
        {
            return File.ReadAllText(path);
        }
    }
}