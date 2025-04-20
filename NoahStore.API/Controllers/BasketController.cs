using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoahStore.API.Errors;
using NoahStore.Core.Entities;
using NoahStore.Core.Interfaces;

namespace NoahStore.API.Controllers
{

    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IBasketRepository basketRepository) : base(unitOfWork, mapper)
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
