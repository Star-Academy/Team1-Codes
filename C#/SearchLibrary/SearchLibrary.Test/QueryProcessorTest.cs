using System.Collections.Generic;
using Moq;
using Xunit;

namespace SearchLibrary.Test
{
    public class QueryProcessorTest
    {
        QueryProcessor queryProcessor;

        public QueryProcessorTest()
        {
            var invertedIndex = CreateMockObject();
            queryProcessor = new QueryProcessor(invertedIndex);
        }

        public InvertedIndex CreateMockObject()
        {
            var invertedIndexMoq = new Mock<InvertedIndex>();
            invertedIndexMoq.SetupGet(r => r.Map)
                .Returns(new Dictionary<string, HashSet<string>>
                {
                    {"five" , new HashSet<string> {"two", "Team"}},
                    {"unit", new HashSet<string> {"One"}},
                    {"test", new HashSet<string> {"One"}},
                    {"feature", new HashSet<string> {"One"}},
                    {"branch", new HashSet<string> {"One", "two"}},
                    {"phase", new HashSet<string> {"Team"}},
                    {"star", new HashSet<string> {"Team"}},
                    {"academy", new HashSet<string> {"Team"}},
                    {"disk", new HashSet<string> {"two"}}
                });

            invertedIndexMoq.SetupGet(i => i.AllFilesNames)
                .Returns(new HashSet<string> { "two", "One", "Team" });
            return invertedIndexMoq.Object;
        }

        [Fact]
        public void AnalyzeTest()
        {
            var rawQuery = "and1 +or1 and2 -exc1 +or2 -exc2 and3";
            var query = queryProcessor.Analyze(rawQuery);

            var expected = new Query();
            expected.AndQueries = new List<string> { "and1", "and2", "and3" };
            expected.OrQueries = new List<string> { "or1", "or2" };
            expected.ExcQueries = new List<string> { "exc1", "exc2" };

            Assert.Equal(expected.AndQueries, query.AndQueries);
            Assert.Equal(expected.OrQueries, query.OrQueries);
            Assert.Equal(expected.ExcQueries, query.ExcQueries);
        }

        [Fact]
        public void SearchQueriesTest()
        {
            var query = new Query();
            query.AndQueries = new List<string> { "branch" };
            query.OrQueries = new List<string> { "five", "test" };
            query.ExcQueries = new List<string> { "phase" };

            var doc = queryProcessor.SearchQueries(query);

            var expected = new Doc();
            expected.AndDocs = new HashSet<string> { "One", "two" };
            expected.OrDocs = new HashSet<string> { "One", "Team", "two" };
            expected.ExcDocs = new HashSet<string> { "Team" };

            Assert.Equal(expected.AndDocs, doc.AndDocs);
            Assert.Equal(expected.OrDocs, doc.OrDocs);
            Assert.Equal(expected.ExcDocs, doc.ExcDocs);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void ProcessTest(string rawQuery, HashSet<string> expected)
        {
            Assert.Equal(expected, queryProcessor.Process(rawQuery));
        }

        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                new object[] {"unit +branch -star", new HashSet<string> {"One"}},
                new object[] {"-branch", new HashSet<string> {"Team"}},
                new object[] {"+five +disk -unit", new HashSet<string> {"two", "Team"}},
                new object[] {"feature -unit", new HashSet<string>()},
                new object[] {"invalid1 +five", new HashSet<string>()},
                new object[] {"+invalid1 five", new HashSet<string>()},
                new object[] {"-invalid1 +five", new HashSet<string> {"two", "Team"}},
                new object[] {"+invalid1 +invalid2", new HashSet<string>()},
                new object[] {"invalid1 invalid2", new HashSet<string>()},
                new object[] {"+invalid1 +invalid2 -invalid3", new HashSet<string>()},
                new object[] {"+invalid1 +invalid2 -five", new HashSet<string>()},
                new object[] {"-invalid1", new HashSet<string> {"One", "two", "Team"}}
            };
    }
}