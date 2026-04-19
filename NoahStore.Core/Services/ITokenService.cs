using NoahStore.Core.Entities.Identity;

namespace NoahStore.Core.Services
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(AppUser appUser);
    }
}
