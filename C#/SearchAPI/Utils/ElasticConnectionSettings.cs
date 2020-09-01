using System;
using Nest;

namespace FirstTimeWeb.Utils
{
    public static class ElasticConnectionSettings
    {
        private const string UriPath = "http://localhost:9200";

        public static ConnectionSettings GetSettings()
        {
            var uri = new Uri(UriPath);
            var connectionSettings = new ConnectionSettings(uri);
#if DEBUG
            connectionSettings.EnableDebugMode();
#endif
            return connectionSettings;
        }  
    }
}