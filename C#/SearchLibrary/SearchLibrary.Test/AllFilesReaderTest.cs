using System.Collections.Generic;
using SearchLibrary.ReadFiles;
using Xunit;

namespace SearchLibrary.Test
{
    public class AllFilesReaderTest
    {
        private const string TestDocumentsPath = @"../../../testDocuments";
        AllFilesReader allFilesReader = new AllFilesReader(TestDocumentsPath);

        [Fact]
        public void GetFilesNameTest()
        {
            var actualNames = new HashSet<string> (allFilesReader.GetAllFilesNames());
            var expectedNames = new HashSet<string> { "Team.txt", "One.txt" };

            Assert.True(actualNames.SetEquals(expectedNames));
        }

        [Fact]
        public void ReadAllFilesTest()
        {
            var actualDict = allFilesReader.ReadAllFiles();
            var expectedTeamTokens = new List<string> { "star", "academy", "phase", "five" };
            var expectedOneTokens = new List<string> { "unit", "test", "feature", "branch" };

            Assert.Equal(expectedTeamTokens, actualDict.GetValueOrDefault("Team.txt"));
            Assert.Equal(expectedOneTokens, actualDict.GetValueOrDefault("One.txt"));
        }
    }
}