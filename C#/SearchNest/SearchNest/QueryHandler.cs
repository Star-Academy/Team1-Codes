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
            client = ElasticClientManager.GetElasticClient();
        }

        public string HandleQuery(string rawQuery, string indexName)
        {
            var searchResponse = SearchQuery(BuildQuery(rawQuery), indexName);

            return ResponseValidator.IsResponseValid(searchResponse)
                ? BuildResult(searchResponse.Documents)
                : "Invalid response";
        }

        private static QueryDescriptor BuildQuery(string rawQuery)
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

        private ISearchResponse<Document> SearchQuery(QueryDescriptor queryDescriptor, string indexName)
        {
            var searchDescriptor = new SearchDescriptor<Document>().Index(Indices.Index(indexName));

            searchDescriptor.Query(q => q
                .Bool(descriptor => descriptor
                    .Must(queryDescriptor.MustFuncList)
                    .Should(queryDescriptor.ShouldFuncList)
                    .MustNot(queryDescriptor.MustNotFuncList)));

            return client.Search<Document>(searchDescriptor);
        }

        private static Func<QueryContainerDescriptor<Document>, QueryContainer> GetContainer(string query)
        {
            return descriptor => descriptor
                .Match(match => match
                    .Field(doc => doc.Text)
                    .Query(query));
        }

        private static string BuildResult(IEnumerable<Document> responseDocuments)
        {
            return responseDocuments.Any()
                ? $"Query was found in {responseDocuments.Select(doc => doc.FileName).Aggregate((x, y) => $"{x}, {y}")}"
                : "Query wasn't found";
        }
    }
}