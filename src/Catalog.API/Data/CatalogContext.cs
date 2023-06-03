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
            var mongoClient = new MongoClient(_configuration.GetSection("ProductDBSettings.ProductDBConnectionString").Value);
            var mongoDB = mongoClient.GetDatabase(_configuration.GetSection("ProductDBSettings.ProductDBName").Value);
            Products = mongoDB.GetCollection<Product>(_configuration.GetSection("ProductDBSettings.ProductDBCollectionName").Value);
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
