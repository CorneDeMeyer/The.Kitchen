namespace The.Kitchen.Domain.Models
{
    public class RecipeConfig(IEnumerable<RecipeConfig> receipeConfig)
    {
        /// <summary>
        /// Configuration injected from Program.cs / Startup
        /// </summary>
        public IEnumerable<RecipeConfig> ReceipeConfigs { get; private set; } = receipeConfig;
    }
}
