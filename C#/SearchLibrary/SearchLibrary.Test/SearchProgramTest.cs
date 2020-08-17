using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace SearchLibrary.Test
{
    public class SearchProgramTest
    {
        [Fact]
        public void SearchTest()
        {
            const string TestDocumentsPath = @"../../../testDocuments";
            var searchProgram = new SearchProgram();
            searchProgram.SetUpInvertIndexSearch(TestDocumentsPath);
            string expected = "Search results: Team.txt";
            Assert.Equal(expected, searchProgram.Search("+five +disk -unit"));
        }
    }
}