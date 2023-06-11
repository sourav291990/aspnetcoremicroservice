using Catalog.API.ConfigurationSettings;
using Catalog.API.Entiities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly ProductDbConfigurationSettings _productDbConfiguration;
        public CatalogContext(IOptions<ProductDbConfigurationSettings> productDbConfiguration)
        {
            _productDbConfiguration = productDbConfiguration.Value;

            string? productDbConnectionString = _productDbConfiguration.ProductDBConnectionString;
            string? productDbName = _productDbConfiguration.ProductDBName; ;
            string? productDbCollectionName = _productDbConfiguration.ProductDBCollectionName; ;

            var mongoClient = new MongoClient(productDbConnectionString);
            var mongoDB = mongoClient.GetDatabase(productDbName);
            Products = mongoDB.GetCollection<Product>(productDbCollectionName);
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
