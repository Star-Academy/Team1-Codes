using System.Collections.Generic;

namespace SearchLibrary.ReadFiles
{
    public interface IReader
    {
         List<string> GetContent(string path);
    }
}