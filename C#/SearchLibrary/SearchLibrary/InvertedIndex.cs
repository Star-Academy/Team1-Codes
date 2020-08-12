using System.Collections.Generic;

namespace SearchLibrary
{
    public class InvertedIndex
    {
        private Dictionary<string, HashSet<string>> _map;
        public Dictionary<string, HashSet<string>> Map 
        {
            get { return _map;}
            private set { _map = value;}
        }
        public void InitMap(Dictionary<string, List<string>> allFiles)
        {
            Map = new Dictionary<string, HashSet<string>>();
            foreach (var fileName in allFiles)
            {
                storeTokens(fileName.Key, fileName.Value);
            }
        }

        public void storeTokens(string fileName, List<string> words)
        {
            foreach (string word in words)
            {
                if (!Map.ContainsKey(word))
                {
                    Map.Add(word, new HashSet<string>() { fileName });
                }
                else
                {
                    HashSet<string> set;
                    Map.TryGetValue(word, out set);
                    set.Add(fileName);
                }
            }
        }
    }
}