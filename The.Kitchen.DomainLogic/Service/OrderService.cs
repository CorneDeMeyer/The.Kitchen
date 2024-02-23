using The.Kitchen.DomainLogic.Interface;
using Microsoft.Extensions.Logging;
using The.Kitchen.Domain.Models;
using The.Kitchen.Domain.Models.Base;
using The.Kitchen.DomainLogic.Constant;

namespace The.Kitchen.DomainLogic.Service
{
    public class OrderService(RecipeConfig recipeConfig,
                              ILoggerFactory loggerFactory) : IOrderService
    {
        private readonly RecipeConfig _config = recipeConfig;
        private readonly ILogger _logger = loggerFactory.CreateLogger<OrderService>();

        public async Task<OrderResponse> RequestOrder(Dictionary<string, int>? ingredient)
        {
            var response = new OrderResponse()
            {
                OriginalIngrediants = ingredient ?? new Dictionary<string, int>(),
            };

            try
            {
                if (IsValidRequest(response))
                {
                    if (_config.ReceipeConfigs != null && _config.ReceipeConfigs.Count() > 0)
                    {
                        var ingrediantsRemaining = response.OriginalIngrediants;
                        var canProcessIngrediants = true;
                        while (canProcessIngrediants)
                        {
                            var receipeFound = await GetReceipeBasedOnIngrediants(ingrediantsRemaining);
                            if (receipeFound != null)
                            {
                                if (true)
                                {

                                }
                            }
                            else
                            {
                                canProcessIngrediants = false;
                            }
                        }
                        response.LeftOverIngrediants = ingrediantsRemaining.Where(i => i.Value > 0).ToDictionary();
                    }
                    else
                    {
                        response.Errors.Add(LoggingMessageConstants.MISSING_CONFIG);
                        _logger.LogWarning(LoggingMessageConstants.MISSING_CONFIG);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingMessageConstants.GENERIC_ERROR);
            }

            return response;
        }

        private async Task<RecipeBase> GetReceipeBasedOnIngrediants(Dictionary<string, int> ingredient)
        {


            return null;
        }

        private bool IsValidRequest(OrderResponse response)
        {
            if (response.OriginalIngrediants == null || response.OriginalIngrediants.Count == 0)
            {
                response.Errors.Add(LoggingMessageConstants.NON_VALID_INGREDIENTS);
                _logger.LogInformation(LoggingMessageConstants.MISSING_CONFIG);
                return false;
            }
            else if (!response.OriginalIngrediants.Any(i => i.Value > 0))
            {
                response.Errors.Add(LoggingMessageConstants.INGREDIENT_AMOUNT_TO_LITTLE);
                _logger.LogInformation(LoggingMessageConstants.INGREDIENT_AMOUNT_TO_LITTLE, response.OriginalIngrediants);
                return false;
            }

            return true;
        }
    }
}
