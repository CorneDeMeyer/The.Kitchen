using The.Kitchen.Domain.Models.Base;
using The.Kitchen.Domain.Models;

namespace The.Kitchen.Tests.FakeData
{
    public static class FakeRecipeConfig
    {
        public static RecipeConfig GetConfig => new RecipeConfig(
            new List<RecipeBase>()
            {
                new RecipeBase()
                {
                    Name = "Burger",
                    Feeds = 1,
                    Ingredients = new Dictionary<string, int> {
                        { "Meat", 1 },
                        { "Lettuce", 1 },
                        { "Tomato", 1 },
                        { "Cheese", 1 },
                        { "Dough", 1 }
                    }
                },
                new RecipeBase()
                {
                    Name = "Pie",
                    Feeds = 1,
                    Ingredients = new Dictionary<string, int> {
                        { "Meat", 2 },
                        { "Dough", 2 }
                    }
                },
                new RecipeBase()
                {
                    Name = "Pasta",
                    Feeds = 2,
                    Ingredients = new Dictionary<string, int> {
                        { "Meat", 1 },
                        { "Tomato", 1 },
                        { "Cheese", 2 },
                        { "Dough", 2 }
                    }
                },
                new RecipeBase()
                {
                    Name = "Salad",
                    Feeds = 3,
                    Ingredients = new Dictionary<string, int> {
                        { "Lettuce", 2 },
                        { "Tomato", 2 },
                        { "Cucumber", 1 },
                        { "Cheese", 2 },
                        { "Olives", 1 }
                    }
                },
                new RecipeBase()
                {
                    Name = "Sandwich",
                    Feeds = 1,
                    Ingredients = new Dictionary<string, int> {
                        { "Cucumber", 1 },
                        { "Dough", 1 }
                    }
                },
                new RecipeBase()
                {
                    Name = "Pizza",
                    Feeds = 4,
                    Ingredients = new Dictionary<string, int> {
                        { "Dough", 3 },
                        { "Tomato", 2 },
                        { "Cheese", 3 },
                        { "Olives", 1 }
                    }
                },
            });
    }
}
