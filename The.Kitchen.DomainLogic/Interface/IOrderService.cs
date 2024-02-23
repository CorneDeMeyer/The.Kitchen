using The.Kitchen.Domain.Models;

namespace The.Kitchen.DomainLogic.Interface
{
    public interface IOrderService
    {
        Task<OrderResponse> RequestOrder(Dictionary<string, int> ingredient);
    }
}
