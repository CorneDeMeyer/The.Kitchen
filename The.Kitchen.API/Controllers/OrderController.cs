using Microsoft.AspNetCore.Mvc;

namespace The.Kitchen.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        [HttpPost("place-order")]
        public IActionResult placeOrder()
        {
            return Ok();
        }
    }
}
