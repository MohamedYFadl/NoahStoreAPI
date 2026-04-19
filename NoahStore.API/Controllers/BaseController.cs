using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoahStore.Core.Interfaces;

namespace NoahStore.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0",Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper mapper;

        public BaseController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
    }
}
