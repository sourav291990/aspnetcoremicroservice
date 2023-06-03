using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entiities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("ProductId")]
        public string Id { get; set; }
        [BsonElement("ProductName")]
        public string Name { get; set; }
        [BsonElement("ProductCategory")]
        public string Category { get; set; }
        [BsonElement("ProductSummary")]
        public string Summary { get; set; }
        [BsonElement("ProductDescription")]
        public string Description { get; set; }
        [BsonElement("ProductImageFile")]
        public string ImageFile { get; set; }
        [BsonElement("ProductPrice")]
        public decimal Price { get; set; }
    }
}
