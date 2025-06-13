using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoahStore.API.Errors;
using NoahStore.Core.Entities;
using NoahStore.Core.Services;

namespace NoahStore.API.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
           _paymentService = paymentService;
        }
        [HttpPost("create-intent")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId,int deliveryMethodId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId, deliveryMethodId);
            if (basket == null) return BadRequest(new ApiResponse(400)); 
            return Ok(basket);
        }
    }
}
