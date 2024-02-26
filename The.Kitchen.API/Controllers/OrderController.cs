using The.Kitchen.DomainLogic.Interface;
using Microsoft.AspNetCore.Mvc;
using The.Kitchen.Domain.Models;

namespace The.Kitchen.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IOrderService orderService) : Controller
    {
        private readonly IOrderService _orderService = orderService;

        /// <summary>
        /// Place an Order
        /// </summary>
        /// <param name="ingrediantsOnHand">Feed in Ingrediants to use</param>
        /// <returns>Object with number of people can be fed, List of Recipes used and a list left over ingrediants</returns>
        [HttpPost("place-order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderResponse>> PlaceOrder([FromBody] Dictionary<string, int> ingrediantsOnHand)
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
