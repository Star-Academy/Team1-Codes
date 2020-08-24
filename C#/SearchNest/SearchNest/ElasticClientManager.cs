using System;
using Nest;

namespace SearchNest
{
    internal static class ElasticClientManager
    {
        private const string UriPath = "http://localhost:9200";
        private static readonly IElasticClient Client = CreateClient();

        private static IElasticClient CreateClient()
        {
            var uri = new Uri(UriPath);
            var connectionSettings = new ConnectionSettings(uri);
            connectionSettings.EnableDebugMode();
            return new ElasticClient(connectionSettings);
        }

        public static IElasticClient GetElasticClient()
        {
            return Client;
        }
    }
}