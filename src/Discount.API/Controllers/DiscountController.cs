using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {

        public DiscountController()
        {
            
        }

        [HttpGet]
        [Route("health")]
        public IActionResult Health()
        {
            return Ok();
        }
    }
}
