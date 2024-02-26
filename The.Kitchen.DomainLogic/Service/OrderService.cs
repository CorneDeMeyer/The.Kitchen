using The.Kitchen.DomainLogic.Interface;
using The.Kitchen.DomainLogic.Constant;
using The.Kitchen.Domain.Models.Base;
using Microsoft.Extensions.Logging;
using The.Kitchen.Domain.Models;

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
                // Validate that the dictionary key is not case sensitive
                OriginalIngrediants = ingredient != null 
                                       ? new Dictionary<string, int>(dictionary: ingredient, comparer: StringComparer.InvariantCultureIgnoreCase) 
                                       : new Dictionary<string, int>(),
            };

            try
            {
                // Check if requst is valid
                if (IsValidRequest(response))
                {
                    // Validate that there is a valid Recipe Configuration available
                    if (_config.ReceipeConfigs != null && _config.ReceipeConfigs.Count() > 0)
                    {
                        // Copy of
                        var ingrediantsRemaining = response.OriginalIngrediants;
                        var canProcessIngrediants = true;
                        while (canProcessIngrediants)
                        {
                            var receipeFound = await GetReceipeBasedOnIngredients(ingrediantsRemaining);
                            if (receipeFound != null)
                            {
                                // Check if Recipe Found was already selected
                                if (response.Orders.ContainsKey(receipeFound.Name))
                                {
                                    response.Orders[receipeFound.Name] += receipeFound.Feeds;
                                }
                                // Simply add the recipe to the order
                                else
                                {
                                    response.Orders.Add(receipeFound.Name, receipeFound.Feeds);
                                }

                                // Deduct Recipe Consumptions from Ingrediants
                                foreach (var ingredientUsed in receipeFound.Ingredients)
                                {
                                    // Validate that we are only removing ingradient we DO have in stock.
                                    if (ingrediantsRemaining[ingredientUsed.Key] - ingredientUsed.Value >= 0)
                                    {
                                        ingrediantsRemaining[ingredientUsed.Key] -= ingredientUsed.Value;
                                    }
                                    else // Throw error that something went wrong with the GetReceipeBasedOnIngrediants functionality (LAST RESORT BACKUP CHECK)
                                    {
                                        canProcessIngrediants = false;
                                        break;
                                    }
                                }
                            }
                            else // We are done with the ingrediants, not enough ingredients to fulfill recipe/s
                            {
                                canProcessIngrediants = false;
                            }
                        }
                        // Display any left over ingredients, if any
                        response.LeftOverIngrediants = ingrediantsRemaining.Where(i => i.Value > 0).ToDictionary();
                    }
                    else // Config is missing, please add recipes to API -> AppSettings.JSON
                    {
                        response.Errors.Add(LoggingMessageConstants.MISSING_CONFIG);
                        _logger.LogWarning(LoggingMessageConstants.MISSING_CONFIG);
                    }
                }
            }
            // Generic Exception Handling done here
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingMessageConstants.GENERIC_ERROR);
            }

            return response;
        }

        private Task<RecipeBase> GetReceipeBasedOnIngredients(Dictionary<string, int> ingredientRemaining)
        {
            if (ingredientRemaining.Any(i => i.Value > 0))
            {
                var recipesCanMake = new List<RecipeBase>();

                // Go through each Recipe
                foreach (var recipe in _config.ReceipeConfigs)
                {
                    var hasAllIngrediants = true;
                    // Iterate through ingrediants required
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        // Check the ingrediant exists and there is enough to make recipe
                        if (ingredientRemaining.ContainsKey(ingredient.Key) 
                         && ingredientRemaining[ingredient.Key] > 0
                         && ingredientRemaining[ingredient.Key] - ingredientRemaining[ingredient.Key] < 0)
                        {
                            hasAllIngrediants = false;
                        }
                    }

                    if (hasAllIngrediants)
                    {
                        recipesCanMake.Add(recipe);
                    }
                }

                // When we do have a recipe we matched, use the one that can feed most people first, then by name (just becuase I want to)
                if (recipesCanMake.Any())
                {
                    return Task.FromResult<RecipeBase>(recipesCanMake.OrderByDescending(r => r.Feeds).ThenBy(r2 => r2.Name).First());
                }
            }
            
            // No Recipe found to return null
            return Task.FromResult<RecipeBase>(null);
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
                _logger.LogInformation(LoggingMessageConstants.INGREDIENT_AMOUNT_TO_LITTLE, [ response.OriginalIngrediants ]);
                return false;
            }

            return true;
        }
    }
}
