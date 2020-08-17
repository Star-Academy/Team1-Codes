using System.Collections.Generic;

namespace SearchLibrary
{
    public class Query
    {
        public List<string> AndQueries { get; set; }
        public List<string> OrQueries { get; set; }
        public List<string> ExcQueries { get; set; }
    }
}