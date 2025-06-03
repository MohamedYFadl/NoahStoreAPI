using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoahStore.API.DTOs;
using NoahStore.API.Errors;
using NoahStore.API.Hepler;
using NoahStore.Core.Dto;
using NoahStore.Core.Entities;
using NoahStore.Core.Interfaces;
using NoahStore.Core.Services;
using NoahStore.Core.Sharing;
using NoahStore.Infrastructure.Repositories;

namespace NoahStore.API.Controllers
{

    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;

        public ProductController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IProductService productService,
            IProductRepository productRepository)
            : base(unitOfWork, mapper)
        {
            _productService = productService;
            _productRepository = productRepository;
        }
        [HttpGet("get-all")]
        public async Task<ActionResult<IReadOnlyList<ProductDTO>>> GetAll([FromQuery] ProductSpecsParams productSpecs)
        {
            var products = await _productService.GetAllProductsAsync(productSpecs);
            if (products == null) return NotFound(new ApiResponse(404,"No products has been founded!"));
            var mappedProducts = mapper.Map<IReadOnlyList<ProductDTO>>(products);

            int productsCount = await _productService.GetCountAsync(productSpecs);
            return Ok(new Pagaination<ProductDTO>(productSpecs.PageIndex,productSpecs.PageSize, productsCount, mappedProducts));
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if(product == null) return NotFound(new ApiResponse(404,$"No product was founded !"));
            var mappedProduct = mapper.Map<ProductDTO>(product);

            return Ok(mappedProduct);
        }

        [HttpPost("delete-by-id/{productId}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> DeleteProductById(int productId)
        {
           var result = await _productService.DeleteProductAsync(productId);

            if (result == false)
                return BadRequest(new ApiResponse(400,"Error occur while deleting"));
            return Ok(new ApiResponse(200, "Item has been deleted successfully"));
        }

        [HttpPost("add-product")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> Add(AddProductDTO productDTO)
        {
            try
            {
                await _productRepository.AddProductAsync(productDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }
    }
}
