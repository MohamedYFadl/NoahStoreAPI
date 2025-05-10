using Microsoft.AspNetCore.Identity;
using NoahStore.Core.Dto;
using NoahStore.Core.Entities.Identity;
using NoahStore.Core.Interfaces;
using NoahStore.Core.Services;
using System.Security.Claims;

namespace NoahStore.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMailService _mailService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthRepository(
            UserManager<AppUser> userManager,
            IMailService mailService,
            SignInManager<AppUser> signInManager,ITokenService tokenService)
        {
            _userManager = userManager;
            _mailService = mailService;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> LoginAsync(LoginDto loginDto)
        {
            if (loginDto == null)
                return null;
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return new AuthResult()
                {
                    Success = false,
                    message = ["Email does not exist"]
                };
            }
            var isPasswordValid = await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);
            if (!isPasswordValid.Succeeded)
            {
                return new AuthResult() { Success = false, message = ["Incorrect Password !"] };
            }
            // Generate Token
            return new AuthResult() {
                Success = true,
                message = ["Login Successfully !"],
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user)
            };
        }
        public async Task<AuthResult> RegisterAsync(RegisterDto registerDto)
        {
            if (registerDto == null)
                return null;

            if (await _userManager.FindByNameAsync(registerDto.UserName) is not null)
            {

                return new AuthResult()
                {
                    Success = false,
                    message = ["This username is already exists!"]
                };
            }
            if (await _userManager.FindByEmailAsync(registerDto.Email) is not null)
            {

                return   new AuthResult()
                {
                    Success = false,
                    message = ["User with this email already exists"]
                };
            }

            var user = new AppUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                DisplayName = registerDto.UserName,
                

            };

            var createdUser = await _userManager.CreateAsync(user,registerDto.Password);
            if(!createdUser.Succeeded)
            {
                return new AuthResult
                {
                    Success = false,
                    message = createdUser.Errors.Select(x => x.Description)
                };
            }
            // Send Message to Activate email
           await SendEmail(registerDto.Email, "Activate Email", "Please active you email by click on link below !");

            return  new AuthResult
            {
                Success = true,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                message = ["The account has been registered successfully"]
            };

        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
        public async Task SendEmail(string email,string subject,string message)
        {
            var result =new EmailDto(email,"Yasovic03@gmail.com",subject,message);
           await  _mailService.SendEmail(result);
        }

        public async Task<AuthResult> LogoutAsync()
        {
           await _signInManager.SignOutAsync();
            return new AuthResult { Success = true,message = ["Logged out successfully"] };
        }

        public async Task<AuthResult> GetUserInfoAsync(string email)
        {
           var user =  await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;

            return new AuthResult
            {
                DisplayName = user.DisplayName, Email = user.Email,
                Success = true,
                
            };


        }
    }
}
