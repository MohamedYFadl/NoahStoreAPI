using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoahStore.API.Errors;
using NoahStore.Core.Entities;
using NoahStore.Core.Interfaces;

namespace NoahStore.API.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(
            IBasketRepository basketRepository) 
        {
            _basketRepository = basketRepository;
        }
        [HttpGet("get-basket-item/{id}")]
        public async Task<IActionResult>Get(string id)
        {
            var result = await _basketRepository.GetBasketAsync(id);
            if(result == null)
            {
                return Ok(new CustomerBasket());
            }
            return Ok(result);
        }
        [HttpPost("update-basket")]
        public async Task<IActionResult> Update(CustomerBasket basket)
        {
            var _basket = await _basketRepository.UpdateBasketAsync(basket);
            return Ok(basket);
        }
        [HttpDelete("delete-basket-item/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _basketRepository.DeleteBasketAsync(id);
            return result ? Ok(new ApiResponse(200, "item has been deleted")) : BadRequest(new ApiResponse(400));
        }
    }
}
