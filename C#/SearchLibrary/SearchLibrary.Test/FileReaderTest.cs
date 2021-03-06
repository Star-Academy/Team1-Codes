using System.Collections.Generic;
using SearchLibrary.ReadFiles;
using Xunit;

namespace SearchLibrary.Test
{
    public class FileReaderTest
    {
        private const string TestDocumentsPath = @"../../../testDocuments";
        FileReader fileReader = new FileReader();

        [Fact]
        public void GetFileContentTest() {
            var actualContent = fileReader.GetContent(TestDocumentsPath + "/Team.txt");
            var expectedContent = "star academy\nphase five"; 
            Assert.Equal(expectedContent, actualContent);
        }
    }
}

