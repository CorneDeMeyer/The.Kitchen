using Microsoft.AspNetCore.Mvc;
using The.Kitchen.API.Controllers;
using The.Kitchen.Domain.Models;
using The.Kitchen.Domain.Models.Base;
using The.Kitchen.Tests.FakeData;

namespace The.Kitchen.Tests.IntergrationTests
{
    public class RecipeControllerTests
    {
        [Fact(DisplayName = "Get Recipe Configuration - Success")]
        public void GetConfig_Success()
        {
            var controller = GetController;

            var result = controller.Get();
            var resultObject = result.Result;

            Assert.NotNull(controller);
            Assert.NotNull(result);

            var okResult = Assert.IsType<OkObjectResult>(resultObject);
            var value = Assert.IsType<List<RecipeBase>>(okResult.Value);

            Assert.NotEmpty(value);

        }

        private RecipeController GetController => new RecipeController(FakeRecipeConfig.GetConfig);
    }
}
