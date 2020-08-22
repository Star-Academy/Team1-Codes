using System;
using Xunit;
using SearchNest.DataReader;
using System.Collections.Generic;

namespace SearchNest.Test
{
    public class DataReaderTest
    {
        private const string TestDocumentsPath = @"../../../testDocuments";
        AllFilesReader allFilesReader = new AllFilesReader(TestDocumentsPath);

        [Fact]
        public void ReadAllFilesTest()
        {
            var actualDict = allFilesReader.ReadAllFiles();
            var expectedTeamText = "Star academy phase eight";
            var expectedOneText = "Elastic search using Nest";

            Assert.Equal(expectedTeamText, actualDict.GetValueOrDefault("Team.txt"));
            Assert.Equal(expectedOneText, actualDict.GetValueOrDefault("One.txt"));
        }
    }
}
