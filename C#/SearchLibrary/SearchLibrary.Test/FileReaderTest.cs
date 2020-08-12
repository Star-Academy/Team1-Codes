using System.Collections.Generic;
using SearchLibrary.ReadFiles;
using Xunit;

namespace SearchLibrary.Test
{
    public class FileReaderTest
    {
        const string TestDocumentsPath = "..\\..\\..\\..\\..\\..\\Resources\\testDocuments";
        FileReader fileReader = new FileReader();

        [Fact]
        public void GetFileContentTest() {
            var actualContent = fileReader.GetContent(TestDocumentsPath + "\\Team.txt");
            var expectedContent = new List<string> {"star", "academy", "phase", "five"}; 
            Assert.Equal(expectedContent, actualContent);
        }

        [Fact]
        public void GetFilesNameTest() {
            
        }
    }
}

