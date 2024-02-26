using The.Kitchen.Tests.SetupService;
using The.Kitchen.API.Controllers;
using The.Kitchen.Tests.FakeData;
using Microsoft.AspNetCore.Mvc;
using The.Kitchen.Domain.Models;

namespace The.Kitchen.Tests.IntergrationTests
{
    public class OrderControllerTests
    {
        [Fact(DisplayName = "Place an Order - Success")]
        public async Task Place_Order_Test_Success()
        {
            var controller = GetController();

            var result = await controller.PlaceOrder(FakeIngredients.GetIngredients);
            var resultObject = result.Result;

            Assert.NotNull(controller);
            Assert.NotNull(result);

            var okResult = Assert.IsType<OkObjectResult>(resultObject);
            var value = Assert.IsType<OrderResponse>(okResult.Value);

            Assert.Empty(value.Errors);
            Assert.Equal(23, value.Feeds);
            Assert.Equal(5, value.LeftOverIngrediants.Count);
        }


        private OrderController GetController() => new OrderController(SetupOrderService.GetService);
    }
}
