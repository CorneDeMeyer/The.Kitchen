namespace The.Kitchen.Domain.Models.Base
{
    public class RecipeBase
    {
        /// <summary>
        /// What recipe will make
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// How many people will this ingradients feed
        /// </summary>
        public required int Feeds { get; set; }

        /// <summary>
        /// What Ingrediants are required for Recipe
        /// </summary>
        public required Dictionary<string, int> Ingredients { get; set; }
    }
}
