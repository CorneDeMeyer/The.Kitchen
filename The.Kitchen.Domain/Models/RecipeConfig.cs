using The.Kitchen.Domain.Models.Base;

namespace The.Kitchen.Domain.Models
{
    public class RecipeConfig(IEnumerable<RecipeBase> receipeConfig)
    {
        /// <summary>
        /// Configuration injected from Program.cs / Startup
        /// </summary>
        public List<RecipeBase> ReceipeConfigs { get; private set; } = receipeConfig.ToList();
    }
}
