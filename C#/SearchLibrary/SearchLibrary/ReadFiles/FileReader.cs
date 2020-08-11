using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchLibrary.ReadFiles
{
    public class FileReader : IReader
    {
        public List<string> GetContent(string path)
        {
            return Regex.Matches(File.ReadAllText(path), "\\w+").Cast<Match>().Select(x => x.Value.Trim()).ToList();
        }
    }
}