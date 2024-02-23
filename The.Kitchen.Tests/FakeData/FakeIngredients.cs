namespace The.Kitchen.Tests.FakeData
{
    public class FakeIngredients
    {
        public static Dictionary<string, int> GetIngredients =>
            new Dictionary<string, int> {
                {
                    "cucumber", 2
                },
                {
                    "Olives", 2
                },
                {
                    "Lettuce", 3
                },
                {
                    "meat", 6
                },
                {
                    "tomato", 6
                },
                {
                    "CheesE", 8
                },
                {
                    "DouGh", 10
                },
            };
    }
}
