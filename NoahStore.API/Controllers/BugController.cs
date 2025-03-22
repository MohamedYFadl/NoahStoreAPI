using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoahStore.API.Errors;
using NoahStore.Core.Entities;
using NoahStore.Core.Interfaces;

namespace NoahStore.API.Controllers
{

    public class BugController : BaseController
    {
        public BugController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        [HttpGet("not-found")]
        public async Task<ActionResult> GetNotFound()
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(0);
            if (category == null) return NotFound(new ApiResponse(404));
            return Ok(category);
        }
        [HttpGet("server-error")]
        public async Task<ActionResult> GetServerError()
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(0);
            category.Name = "";
            return Ok();
        }
        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("bad-request/{id}")] // we will send to it string not int (Validation Error)
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
        [HttpGet("unauthorized")]
        public ActionResult GetUnAuthorizedError()
        {
            return Unauthorized(new ApiResponse(401));
        }
    }
}
