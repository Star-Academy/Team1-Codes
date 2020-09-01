using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FirstTimeWeb.Models
{
    public class SearchPattern
    {
        [JsonPropertyName("and")]
        public IEnumerable<string> AndQueries { get; set; }
        [JsonPropertyName("or")]
        public IEnumerable<string> OrQueries { get; set; }
        [JsonPropertyName("exc")]
        public IEnumerable<string> ExcQueries { get; set; }
    }
}