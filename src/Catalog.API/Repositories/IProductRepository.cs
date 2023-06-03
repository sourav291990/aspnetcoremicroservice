using Catalog.API.Entiities;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductByCategory(string category);
        Task<IEnumerable<Product>> GetProductByName(string name);

        Task<Product> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
