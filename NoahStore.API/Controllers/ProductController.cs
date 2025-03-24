using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoahStore.API.Errors;
using NoahStore.Core.Entities;
using NoahStore.Core.Interfaces;
using NoahStore.Infrastructure.Repositories;

namespace NoahStore.API.Controllers
{

    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }
        [HttpGet("get-all")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetAll()
        {
            var products = await _unitOfWork.Repository<Product>().GetAllAsync();
            if (products == null) return BadRequest(new ApiResponse(400));
            return Ok(products);
        }
        [HttpGet("get-by-id-{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            if(product == null) return NotFound(new ApiResponse(404,$"No product was founded !"));
            return Ok(product);
        }
    }
}
