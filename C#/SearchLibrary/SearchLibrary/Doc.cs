using System.Collections.Generic;

namespace SearchLibrary
{
    public class Doc
    {
        public Doc()
        {
            AndDocs = new HashSet<string>();
            OrDocs = new HashSet<string>();
            ExcDocs = new HashSet<string>();
        }
        
        public bool IncompatibleAnds { get; set; }
        public bool IncompatibleOrs { get; set; }

        public HashSet<string> AndDocs { get; set; }
        public HashSet<string> OrDocs { get; set; }
        public HashSet<string> ExcDocs { get; set; }
    }
}