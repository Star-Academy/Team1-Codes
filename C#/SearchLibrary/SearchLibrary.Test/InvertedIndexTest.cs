using System.Collections.Generic;
using Xunit;

namespace SearchLibrary.Test
{
    public class InvertedIndexTest
    {
        InvertedIndex inv = new InvertedIndex();
        [Fact]
        public void initMapTest()
        {
            string key = "file";
            List<string> value = new List<string> { "word1", "word2" };
            inv.InitMap(new Dictionary<string, List<string>> { { key, value } });
            Dictionary<string, HashSet<string>> expected =
                    new Dictionary<string, HashSet<string>> {
                        { "word1", new HashSet<string>{key} } ,
                        { "word2", new HashSet<string>{key} }
                    };
            Assert.Equal(expected, inv.Map);
        }

    }
}