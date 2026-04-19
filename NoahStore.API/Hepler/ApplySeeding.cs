using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoahStore.Core.Entities.Identity;
using NoahStore.Infrastructure.Data.Config;
using NoahStore.Infrastructure.Data.DbContexts;

namespace NoahStore.API.Hepler
{
    public  class ApplySeeding
    {
        public async static Task  ApplySeedingAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
            try
            {
                var _dbContext = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();  
                await _dbContext.Database.MigrateAsync();
                await ApplicationDbContextSeed.SeedAsync(_dbContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error has been occured during apply migration");

            }


             
        }
    }
}
