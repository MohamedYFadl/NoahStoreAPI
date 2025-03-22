using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoahStore.API.Errors;

namespace NoahStore.API.Controllers
{
    [Route("Errors/{statuscode}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error(int statuscode)
        {
            return new ObjectResult(new ApiResponse(statuscode));
        }
    }
}
