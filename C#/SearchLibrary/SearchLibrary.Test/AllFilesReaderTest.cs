using System.Collections.Generic;
using SearchLibrary.ReadFiles;
using Xunit;

namespace SearchLibrary.Test
{
    public class AllFilesReaderTest
    {
        const string TestDocumentsPath = "..\\..\\..\\..\\..\\..\\Resources\\testDocuments";

        [Fact]
        public void GetFilesNameTest() {
            var allFilesReader = new AllFilesReader(TestDocumentsPath);
            var actualNames = allFilesReader.getAllFilesNames();
            var expectedNames = new List<string> {"Team.txt", "One.txt"};
            Assert.Equal(expectedNames, actualNames);
        }
    }
}