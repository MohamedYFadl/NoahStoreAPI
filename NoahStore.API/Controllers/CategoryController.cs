using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoahStore.API.Errors;
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
        public async Task<ActionResult<IReadOnlyList<Category>>> GetAll()
        {
            var Categories = await _unitOfWork.Repository<Category>().GetAllAsync();
            if (Categories == null) return BadRequest(new ApiResponse(400));
            return Ok(Categories);
        }
        [HttpGet("get-by-id-{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            if(category == null) return BadRequest(new ApiResponse(404,$"No Category was founded with id = {id} "));
            return Ok(category);
        }
    }
}
