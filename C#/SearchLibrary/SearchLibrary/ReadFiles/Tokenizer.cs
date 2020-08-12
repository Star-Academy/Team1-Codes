using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchLibrary.ReadFiles
{
    internal class Tokenizer
    {
        private string pattern;

        public Tokenizer(string pattern)
        {
            this.pattern = pattern;
        }

        internal List<string> getTokens(string text) {
            return Regex.Matches(text, pattern).Cast<Match>().Select(x => x.Value.Trim()).ToList();
        }
    }
}