using System.Collections.Generic;
using Nest;

namespace SearchNest
{
    public class Importer<T> where T : class
    {
        private readonly IElasticClient client;

        public Importer()
        {
            this.client = ElasticClientManager.GetElasticClient();
        }
        public void importData(string indexName, IEnumerable<T> documents) {
            client.Bulk(CreateBulk(indexName, documents));
        }

        private BulkDescriptor CreateBulk(string indexName, IEnumerable<T> documents)
        {
            var bulkDescriptor = new BulkDescriptor();
            foreach (var document in documents)
            {
                bulkDescriptor.Index<T>(x => x
                    .Index(indexName)
                    .Document(document)
                );
            }
            return bulkDescriptor;
        }
    }
}