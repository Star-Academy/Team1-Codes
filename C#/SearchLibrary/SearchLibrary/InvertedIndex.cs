using System.Collections.Generic;

namespace SearchLibrary
{
    /// <summary>
    /// InvertedIndex objects are initialized using InitMap
    /// which stores all the tokens read from the documents
    /// </summary>
    public class InvertedIndex
    {
        public virtual HashSet<string> AllFilesNames { get; set; }
        private Dictionary<string, HashSet<string>> map;
        public virtual Dictionary<string, HashSet<string>> Map
        {
            get { return map; }
            private set { map = value; }
        }

        public void InitMap(Dictionary<string, List<string>> allFiles)
        {
            AllFilesNames = new HashSet<string>(allFiles.Keys);

            Map = new Dictionary<string, HashSet<string>>();
            foreach (var fileName in allFiles)
            {
                StoreTokens(fileName.Key, fileName.Value);
            }
        }

        public void StoreTokens(string fileName, List<string> words)
        {
            foreach (var word in words)
            {
                HashSet<string> set;
                if (Map.TryGetValue(word, out set))
                    set.Add(fileName);
                else
                    Map.Add(word, new HashSet<string>() { fileName });
            }
        }
    }
}