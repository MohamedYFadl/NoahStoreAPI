using NoahStore.Core.Entities;

namespace NoahStore.Core.Services
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId, int? deliveryMethodId);
    }
}
