using System;
using System.Linq;
using Nest;
using SearchNest.Model;

namespace SearchNest
{
    public class QueryHandler
    {
        private readonly IElasticClient client;

        public QueryHandler()
        {
            this.client = ElasticClientManager.GetElasticClient();
        }

        public string HandleQuery(string rawQuery, string indexName)
        {
            return BuildResult(BuildQuery(rawQuery), indexName);
        }

        public QueryDescriptor BuildQuery(string rawQuery)
        {
            var queryDescriptor = new QueryDescriptor();
            var splitQuery = rawQuery.Split(" ");
            
            queryDescriptor.ShouldFuncList = splitQuery.Where(q => q.StartsWith("+"))
                .Select(query => GetContainer(query.Substring(1))).ToList();
            queryDescriptor.MustNotFuncList = splitQuery.Where(q => q.StartsWith("-"))
                .Select(query => GetContainer(query.Substring(1))).ToList();
            queryDescriptor.MustFuncList = splitQuery.Where(q => !q.StartsWith("+") && !q.StartsWith("-"))
                .Select(GetContainer).ToList();

            return queryDescriptor;
        }

        public string BuildResult(QueryDescriptor queryDescriptor, string indexName)
        {
            var searchDescriptor = new SearchDescriptor<Document>().Index(Indices.Index(indexName));

            searchDescriptor.Query(q => q
                .Bool(descriptor => descriptor
                    .Must(queryDescriptor.MustFuncList)
                    .Should(queryDescriptor.ShouldFuncList)
                    .MustNot(queryDescriptor.MustNotFuncList)));

            var response = client.Search<Document>(searchDescriptor);

            return response.Documents.Any()
                ? $"Query was found in {response.Documents.Select(doc => doc.FileName).Aggregate((x, y) => $"{x}, {y}")}"
                : "Query wasn't found";
        }

        private static Func<QueryContainerDescriptor<Document>, QueryContainer> GetContainer(string query)
        {
            return descriptor => descriptor
                .Match(match => match
                    .Field(doc => doc.Text)
                    .Query(query));
        }
    }
}