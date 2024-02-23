using The.Kitchen.DomainLogic.Interface;
using Microsoft.AspNetCore.Mvc;

namespace The.Kitchen.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IOrderService orderService) : Controller
    {
        private readonly IOrderService _orderService = orderService;

        [HttpPost("place-order")]
        public async Task<IActionResult> placeOrder([FromBody] Dictionary<string, int> ingrediantsOnHand)
        {
            var response = await _orderService.RequestOrder(ingrediantsOnHand);

            if (response.Errors.Any())
            {
                return BadRequest(string.Join(", ", response.Errors));
            }

            return Ok(response);
        }
    }
}
