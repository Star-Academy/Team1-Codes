using System.IO;
using System.Text.Json;

namespace Project02
{
    public class ReadData {
         public static T[] Read <T> (string src) {
             return JsonSerializer.Deserialize<T[]>(File.ReadAllText(src));
         }
    }
}