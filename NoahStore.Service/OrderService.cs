using AutoMapper;
using NoahStore.Core.Dto;
using NoahStore.Core.Entities;
using NoahStore.Core.Entities.Order;
using NoahStore.Core.Interfaces;
using NoahStore.Core.Services;
using NoahStore.Core.Specifications;

namespace NoahStore.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public OrderService(
            IUnitOfWork unitOfWork,
            IBasketRepository basketRepository,
            IPaymentService paymentService,
            IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
            _paymentService = paymentService;
            _mapper = mapper;
        }

        public async Task<bool> CancelOrderAsync(int orderId, string BuyerEmail)
        {
            var order =await _unitOfWork.Repository<Orders>().GetByIdAsync( orderId );
            if(order != null && order.BuyerEmail == BuyerEmail)
            {
                order.OrderStatus = OrderStatus.Cancelled;
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public async Task<Orders> CreateOrderAsync(OrderDto orderDto, string BuyerEmail)
        {
            #region 1.Get orderItems from the basket
            var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId);

            List<OrderItems> orderItems = new List<OrderItems>();
            if (basket != null)
            {
                foreach (var item in basket.BasketItems)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var orderItem = new OrderItems(product.Price, item.Quantity,
                        product.Id, product.Name, item.PictureUrl);
                    product.StockQuantity -= item.Quantity;
                    orderItems.Add(orderItem);
                }
            }
            #endregion

            #region 2. Get DeliveryMethod of this order
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(orderDto.DeliveryMethodId);

            #endregion

            #region 3. Calc SubTotal
            var subtotal = orderItems.Sum(m => m.Price * m.Quantity);

            #endregion

            #region 4. Get Shipping Address
            var shippAddress = _mapper.Map<ShippingAddress>(orderDto.shippingAddressDto);

            #endregion

            #region 5.Check if the order existed or not 

            var orderRepo =  _unitOfWork.Repository<Orders>();
            var orderIntentSpecs = new OrderWithPaymentIntentSpecs(basket.PaymentIntentId);
            var exisitingOrder = await orderRepo.GetByIdAsync(orderIntentSpecs);
            if(exisitingOrder != null)
            {
                await orderRepo.RemoveAsync(exisitingOrder.Id);
                await _paymentService.CreateOrUpdatePaymentIntent(basket.Id, deliveryMethod.Id);
            }
            #endregion


            #region 6. Create Order
            var order = new Orders(BuyerEmail, subtotal, shippAddress, deliveryMethod, orderItems,basket.PaymentIntentId);

            #endregion

            #region 7. Add the order to db
            
            await orderRepo.AddAsync(order);
            _unitOfWork.Save();

            #endregion

            #region 8. Delete the basket from Redis after creating the order
            await _basketRepository.DeleteBasketAsync(orderDto.BasketId);

            #endregion

            return order;
        }

        public async Task<IReadOnlyList<Orders>> GetAllOrdersForUserAsync(string BuyerEmail)
        {
            var orderspecs = new OrderSpecification( BuyerEmail);
            var allUserOrders = await _unitOfWork.Repository<Orders>().GetAllAsync(orderspecs);
            if (allUserOrders.Count == 0)
                return null;
            return allUserOrders;

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
            => await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

        public async Task<Orders> GetOrderByIdAsync(int id, string BuyerEmail)
        {

            var orderspecs = new OrderSpecification(id, BuyerEmail);
            var order =await _unitOfWork.Repository<Orders>().GetByIdAsync(orderspecs);
            if(order == null)
                return null;
            return order;
        }
    }
}
