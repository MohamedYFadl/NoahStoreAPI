using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoahStore.API.Errors;
using NoahStore.Core.Dto;
using NoahStore.Core.Interfaces;
using System.Security.Claims;

namespace NoahStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AccountController(IAuthRepository authRepository)
        {
           _authRepository = authRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResult>> Login(LoginDto input)
        {
            if (input == null)
                return BadRequest(new ApiResponse(400, "Email doesn't exist"));

           var result =  await _authRepository.LoginAsync(input);

            return Ok(result);

        }
        [HttpPost("register")]
        public async Task<ActionResult<AuthResult>> Register(RegisterDto input)
        {
            if (input == null)
                return BadRequest(new ApiResponse(400, "Email doesn't exist"));

            var result = await _authRepository.RegisterAsync(input);

            return Ok(result);

        }
        [HttpGet("get-user-info")]
        [Authorize]
        public async Task<ActionResult<AuthResult>> GetCurrentUserInfo()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);
            var user = await _authRepository.GetUserInfoAsync(userEmail.Value);
           
            if(user == null)
                return BadRequest(new ApiResponse(400));
            return Ok(user);
        }
    }
}
