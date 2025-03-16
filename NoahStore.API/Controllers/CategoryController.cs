using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoahStore.Core.Entities;
using NoahStore.Core.Interfaces;

namespace NoahStore.API.Controllers
{

    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var Categories = await _unitOfWork.Repository<Category>().GetAllAsync();
            if (Categories == null) return BadRequest();
            return Ok(Categories);
        }
        [HttpGet("get-by-id-{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            if(category == null) return BadRequest();
            return Ok(category);
        }
    }
}
