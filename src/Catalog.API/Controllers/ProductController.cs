using Catalog.API.Entiities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Catalog.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private ILogger<ProductController> _logger;

        public ProductController(IProductRepository repository, ILogger<ProductController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _repository.GetProducts());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var result = await _repository.GetProduct(id);
            if (null != result)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("name")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName([FromQuery] string name)
        {
            var result = await _repository.GetProductByName(name);
            if (null != result && result.Any())
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("category")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory([FromQuery] string category)
        {
            var result = await _repository.GetProductByCategory(category);
            if (null != result && result.Any())
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            return Ok(await _repository.CreateProduct(product));
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateProduct(Product product)
        {
            return Ok(await _repository.UpdateProduct(product));
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteProduct(string id)
        {
            return Ok(await _repository.DeleteProduct(id));
        }

    }
}
