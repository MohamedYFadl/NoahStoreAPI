using Microsoft.AspNetCore.Identity;
using NoahStore.Core.Dto;
using NoahStore.Core.Entities.Identity;
using NoahStore.Core.Interfaces;

namespace NoahStore.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
                    message = ["User does not exist"]
                };
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                return new AuthResult() { Success = false, message = ["Incorrect Password !"] };
            }
            // Generate Token
            return new AuthResult() { Success = true, message = ["Login Successfully !"] };
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
            return  new AuthResult
            {
                Success = true,
                message = ["The account has been registered successfully"]
            };

        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}
