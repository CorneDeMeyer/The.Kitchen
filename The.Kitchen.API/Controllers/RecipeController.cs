using The.Kitchen.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using The.Kitchen.DomainLogic.Constant;
using The.Kitchen.Domain.Models.Base;

namespace The.Kitchen.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController(RecipeConfig config) : Controller
    {
        private readonly RecipeConfig _config = config;

        /// <summary>
        /// Current Receipes that can be used to determine what food order can feed as many people as possible
        /// </summary>
        /// <returns>Current Receipes List</returns>
        [HttpGet]
        public ActionResult<List<RecipeBase>> Get()
        {
            if (!_config.ReceipeConfigs.Any())
            {
                return BadRequest(LoggingMessageConstants.MISSING_CONFIG);
            }
            return Ok(_config.ReceipeConfigs);
        }
    }
}
