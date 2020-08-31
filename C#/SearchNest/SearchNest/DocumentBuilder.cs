using SearchNest.Model;
using System.Collections.Generic;
using System.Linq;

namespace SearchNest
{
    public class DocumentBuilder
    {
        public static IEnumerable<Document> BuildDocuments(Dictionary<string, string> filesContents)
        {
            return filesContents.Select(k => new Document {FileName = k.Key, Text = k.Value});
        }
    }
}