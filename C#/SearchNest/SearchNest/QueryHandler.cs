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
        private string IndexName;

        public QueryHandler(string indexName)
        {
            IndexName = indexName;
            this.client = ElasticClientManager.GetElasticClient();
        }

        public string handleQuery(string rawQuery)
        {
            var searchDescriptor = new SearchDescriptor<Document>().Index(Indices.Index(IndexName));

            var mustFuncList = new List<Func<QueryContainerDescriptor<Document>, QueryContainer>>();
            var mustNotFuncList = new List<Func<QueryContainerDescriptor<Document>, QueryContainer>>();
            var shouldFuncList = new List<Func<QueryContainerDescriptor<Document>, QueryContainer>>();

            foreach (var query in rawQuery.Split(" "))
            {
                switch (query[0])
                {
                    case '+':
                        shouldFuncList.Add(GetContainer(query.Substring(1)));
                        break;
                    case '-':
                        mustNotFuncList.Add(GetContainer(query.Substring(1)));
                        break;
                    default:
                        mustFuncList.Add(GetContainer(query));
                        break;
                }
            }

            searchDescriptor.Query(q => q
                .Bool(descriptor => descriptor
                    .Must(mustFuncList)
                    .Should(shouldFuncList)
                    .MustNot(mustNotFuncList)));

            var response = client.Search<Document>(searchDescriptor);
            
            var result = response.Documents
                .Select(doc => doc.FileName)
                .Aggregate((x, y) => $"{x}, {y}");

            return response.Documents.Any()
                ? $"Result is {result}"
                : "Query not found";
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