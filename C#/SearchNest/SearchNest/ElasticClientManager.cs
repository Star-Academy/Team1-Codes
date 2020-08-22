using System;
using Nest;

namespace SearchNest
{
    internal static class ElasticClientManager
    {
        private static IElasticClient Client = CreateClient();

        private static IElasticClient CreateClient()
        {
            var uri = new Uri("http://localhost:9200");
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