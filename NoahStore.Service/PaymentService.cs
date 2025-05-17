using Microsoft.Extensions.Configuration;
using NoahStore.Core.Entities;
using NoahStore.Core.Entities.Order;
using NoahStore.Core.Interfaces;
using NoahStore.Core.Services;
using Stripe;
using Product = NoahStore.Core.Entities.Product;

namespace NoahStore.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        private readonly IConfiguration _configuration;

        public PaymentService(
            IUnitOfWork unitOfWork,
            IBasketRepository basketRepository,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
            _configuration = configuration;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId,int? deliveryMethodId)
        {

            #region 1.Set ApiKey
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
            #endregion

            #region 2.Get Basket
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket == null) return null;
            #endregion

            #region 3.Check basket items price
            if(basket.BasketItems.Count > 0)
            {
                foreach (var item in basket.BasketItems)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);// Id !!
                    if(product.Price != item.UnitPrice)
                        item.UnitPrice = product.Price;
                }
            }

            #endregion

            #region 4. Add Shipping Cost
            var shippingPrice = 0m;
            if (deliveryMethodId.HasValue )
            {
                var delivery =await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId.Value);
                if(delivery is not null)
                { 
                    basket.ShippingPrice = delivery.Price;
                    shippingPrice = delivery.Price;
                }
            }
            #endregion

            #region 5. Create/Update Payment Intent
            PaymentIntent paymentIntent;
            PaymentIntentService paymentIntentService = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            { // Create PaymentIntent
                var createdOptions = new PaymentIntentCreateOptions
                {
                    Amount = (long) shippingPrice *100 + (long)basket.BasketItems.Sum(b=>b.UnitPrice * b.Quantity) * 100 ,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card"}
                };
                paymentIntent = await paymentIntentService.CreateAsync(createdOptions);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }else
            {// Update PaymentIntent
                var updatedOptions = new PaymentIntentUpdateOptions
                {
                    Amount = (long)shippingPrice + (long)basket.BasketItems.Sum(b => b.UnitPrice * b.Quantity) * 100,
                };
                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, updatedOptions);
            }
            await _basketRepository.UpdateBasketAsync(basket);
            return basket;

            #endregion

        }
    }
}
