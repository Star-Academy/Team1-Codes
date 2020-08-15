using System.Collections.Generic;
using SearchLibrary.ReadFiles;
using Xunit;

namespace SearchLibrary.Test
{
    public class AllFilesReaderTest
    {
        const string TestDocumentsPath = "..\\..\\..\\..\\..\\..\\Resources\\testDocuments";
        AllFilesReader allFilesReader = new AllFilesReader(TestDocumentsPath);

        [Fact]
        public void GetFilesNameTest() {
            var actualNames = allFilesReader.getAllFilesNames();
            var expectedNames = new List<string> {"Team.txt", "One.txt"};
            Assert.Equal(expectedNames, actualNames);
        }

        [Fact]
        public void ReadAllFilesTest() {
            var actualDict = allFilesReader.ReadAllFiles();
            var expectedTeamTokens = new List<string> {"star", "academy", "phase", "five"};
            var expectedOneTokens = new List<string> {"unit", "test", "feature", "branch"};
            
            Assert.Equal(expectedTeamTokens, actualDict.GetValueOrDefault("Team.txt"));
            Assert.Equal(expectedTeamTokens, actualDict.GetValueOrDefault("One.txt"));
        }
    }
}