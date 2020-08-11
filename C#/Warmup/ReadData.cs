using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace Project02
{
    public class ReadData
    {
        public List<T> Read<T>(string src)
        {
            return JsonSerializer.Deserialize<List<T>>(File.ReadAllText(src));
        }
    }
}