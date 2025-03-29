using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoahStore.API.DTOs;
using NoahStore.API.Errors;
using NoahStore.API.Hepler;
using NoahStore.Core.Entities;
using NoahStore.Core.Interfaces;
using NoahStore.Core.Specifications;
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
        public async Task<ActionResult<IReadOnlyList<ProductDTO>>> GetAll([FromQuery] ProductSpecsParams productSpecs)
        {
            var specs = new ProductWithImagesAndCategory(productSpecs);
            var products = await _unitOfWork.Repository<Product>().GetAllAsync(specs);
            if (products == null) return BadRequest(new ApiResponse(400));
            var mappedProducts = mapper.Map<IReadOnlyList<ProductDTO>>(products);
            var productsCount = _unitOfWork.Repository<Product>().Count();
            return Ok(new Pagaination<ProductDTO>(productSpecs.PageIndex,productSpecs.PageSize, productsCount, mappedProducts));
        }
        [HttpGet("get-by-id-{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var specs = new ProductWithImagesAndCategory(id);
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(specs);
            if(product == null) return NotFound(new ApiResponse(404,$"No product was founded !"));
            var mappedProduct = mapper.Map<ProductDTO>(product);

            return Ok(mappedProduct);
        }
    }
}
