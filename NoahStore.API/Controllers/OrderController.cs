using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoahStore.API.Errors;
using NoahStore.Core.Dto;
using NoahStore.Core.Entities.Order;
using NoahStore.Core.Services;
using System.Security.Claims;

namespace NoahStore.API.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost("Create-order")]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

           var order =await  _orderService.CreateOrderAsync(orderDto, email);
            if (order == null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }
        [HttpGet("get-order-by-id-{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderByIdAsync(id, email);
            if (order == null)
                return NotFound(new ApiResponse(404, "No Order has been founded"));
            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }

        [HttpGet("get-all-deliverymethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>>GetDeliveryMethodsAsync()
            => Ok(await _orderService.GetDeliveryMethodsAsync());

        [HttpGet("get-user-orders")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetAllOrdersForUserAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetAllOrdersForUserAsync(email);
            if (orders == null)
                return NotFound(new ApiResponse(404, "No orders has been found"));

            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpPost("Cancel-order-{orderId}")]
        public async Task<ActionResult<bool>> CancelOrder(int orderId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email); 

            var result =await _orderService.CancelOrderAsync(orderId, email);

            return Ok(result ? new ApiResponse(200, "Order has been cancelled") : new ApiResponse(404, "No order has been found with this Id"));
        }

    }
}
