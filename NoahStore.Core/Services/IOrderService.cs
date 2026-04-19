using NoahStore.Core.Dto;
using NoahStore.Core.Entities.Order;

namespace NoahStore.Core.Services
{
    public interface IOrderService
    {
        Task<Orders> CreateOrderAsync(OrderDto orderDto,string  BuyerEmail);
        Task<IReadOnlyList<Orders>> GetAllOrdersForUserAsync(string BuyerEmail);
        Task<Orders> GetOrderByIdAsync(int id,string BuyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
        Task<bool> CancelOrderAsync(int orderId, string BuyerEmail);
    }
}
