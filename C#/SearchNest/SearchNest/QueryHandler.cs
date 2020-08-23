using System;
using System.Collections.Generic;
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

        public string handleQuery(string rawQuery, string indexName)
        {
            return BuildResult(BuildQuery(rawQuery), indexName);
        }

        public QueryDescriptor BuildQuery(string rawQuery)
        {
            QueryDescriptor queryDescriptor = new QueryDescriptor();

            var splitQuery = rawQuery.Split(" ");
            queryDescriptor.shouldFuncList = splitQuery.Where(q => q.StartsWith("+"))
                .Select(query => GetContainer(query.Substring(1))).ToList();
            queryDescriptor.mustNotFuncList = splitQuery.Where(q => q.StartsWith("-"))
                .Select(query => GetContainer(query.Substring(1))).ToList();
            queryDescriptor.mustFuncList = splitQuery.Where(q => !q.StartsWith("+") && !q.StartsWith("-"))
                .Select(GetContainer).ToList();

            return queryDescriptor;
        }

        public string BuildResult(QueryDescriptor queryDescriptor, string IndexName)
        {
            var searchDescriptor = new SearchDescriptor<Document>().Index(Indices.Index(IndexName));

            searchDescriptor.Query(q => q
                .Bool(descriptor => descriptor
                    .Must(queryDescriptor.mustFuncList)
                    .Should(queryDescriptor.shouldFuncList)
                    .MustNot(queryDescriptor.mustNotFuncList)));

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

        private static string GenerateResult(IEnumerable<Document> responseDocuments)
        {
            return responseDocuments.Any()
                ? $"Query was found in {responseDocuments.Select(doc => doc.FileName).Aggregate((x, y) => $"{x}, {y}")}"
                : "Query wasn't found";
        }
}
}