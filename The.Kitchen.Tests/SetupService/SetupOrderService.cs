using Microsoft.Extensions.Logging.Abstractions;
using The.Kitchen.DomainLogic.Service;
using The.Kitchen.Tests.FakeData;

namespace The.Kitchen.Tests.SetupService
{
    public static class SetupOrderService
    {
        public static OrderService GetService => new OrderService(FakeRecipeConfig.GetConfig, new NullLoggerFactory());
    }
}
