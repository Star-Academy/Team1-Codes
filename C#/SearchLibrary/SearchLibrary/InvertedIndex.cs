using System.Collections.Generic;

namespace SearchLibrary
{
    /// <summary>
    /// InvertedIndex objects are initialized using InitMap
    /// which stores all the tokens read from the documents
    /// </summary>
    public class InvertedIndex
    {
        public virtual HashSet<string> AllFilesNames { get; protected set; }
        public virtual Dictionary<string, HashSet<string>> Map { get; private set; }

        public void InitMap(Dictionary<string, List<string>> allFiles)
        {
            AllFilesNames = new HashSet<string>(allFiles.Keys);

            Map = new Dictionary<string, HashSet<string>>();
            foreach (var fileName in allFiles)
            {
                StoreTokens(fileName.Key, fileName.Value);
            }
        }

        private void StoreTokens(string fileName, IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                if (Map.TryGetValue(word, out var set))
                    set.Add(fileName);
                else
                    Map.Add(word, new HashSet<string> { fileName });
            }
        }
    }
}