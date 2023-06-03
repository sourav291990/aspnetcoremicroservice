using Catalog.API.Entiities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly IConfiguration _configuration;
        public CatalogContext(IConfiguration configuration)
        {
            _configuration = configuration;
            var mongoClient = new MongoClient(_configuration.GetValue<string>("ProductDBSettings.ProductDBConnectionString"));
            var mongoDB = mongoClient.GetDatabase(_configuration.GetValue<string>("ProductDBSettings.ProductDBName"));
            Products = mongoDB.GetCollection<Product>(_configuration.GetValue<string>("ProductDBSettings.ProductDBCollectionName"));
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
