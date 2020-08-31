using System;
using Nest;

namespace SearchNest
{
    internal static class ElasticClientManager
    {
        private static IElasticClient client;
        private const string UriPath = "http://localhost:9200";

        public static IElasticClient GetElasticClient()
        {
            return client ??= CreateNewClient();
        }

        private static IElasticClient CreateNewClient()
        {
            var uri = new Uri(UriPath);
            var connectionSettings = new ConnectionSettings(uri);
#if DEBUG
            connectionSettings.EnableDebugMode();
#endif
            return new ElasticClient(connectionSettings);
        }
    }
}