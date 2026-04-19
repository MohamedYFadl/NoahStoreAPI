using AdminDashboard.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoahStore.Core.Dto;
using NoahStore.Core.Entities;
using NoahStore.Core.Interfaces;
using NoahStore.Core.Services;
using NoahStore.Core.Sharing;

namespace AdminDashboard.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productService = productService;
        }
        public async Task<IActionResult> Index(int? categoryId = null, string? Term = null, string? Sort = null, int pageIndex = 1, int pageSize = 10)
        {
            var categories = await _unitOfWork.Repository<Category>().GetAllAsync();
            ProductSpecsParams productspecs;
            if (!categoryId.HasValue)
            {
                productspecs = new ProductSpecsParams
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Search = Term,
                    Sort = Sort,
                };
            }
            else
            {
                productspecs = new ProductSpecsParams
                {
                    CategoryId = categoryId,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Search = Term,
                    Sort = Sort,
                };
            }

            var filteredProducts = await _productService.GetAllProductsAsync(productspecs);
            var TotalProductsCount = await _productService.GetCountAsync(productspecs);
            var mappedProducts = _mapper.Map<IReadOnlyList<ProductsViewModel>>(filteredProducts);

            var model = new ProductsListViewModel
            {
                Categories = new SelectList(categories,"Id","Name"),
                Products = mappedProducts,
                 PageSize=pageSize,
                 PageIndex=pageIndex,
                 TotalItems = TotalProductsCount,
                 TotalPages = (int)Math.Ceiling((double)TotalProductsCount / pageSize),
                 Sort = Sort,
                 Term = Term,
                 CategoryId=categoryId,


            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UpsertProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedProduct = _mapper.Map<AddProductDTO>(model);

               var product =  await _productService.AddProductAsync(mappedProduct);

                ViewData["Success"] = "Product has been created successfully!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
