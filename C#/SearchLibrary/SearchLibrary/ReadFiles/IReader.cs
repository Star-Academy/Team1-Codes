using System.Collections.Generic;

namespace SearchLibrary.ReadFiles
{
    public interface IReader
    {
        string GetContent(string path);
    }
}