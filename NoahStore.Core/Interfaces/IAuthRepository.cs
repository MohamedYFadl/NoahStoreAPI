using NoahStore.Core.Dto;

namespace NoahStore.Core.Interfaces
{
    public interface IAuthRepository
    {
        public Task<AuthResult> RegisterAsync(RegisterDto registerDto);
        Task<AuthResult> LoginAsync(LoginDto loginDto);
        Task<bool> UserExistsAsync(string email);

    }
}
