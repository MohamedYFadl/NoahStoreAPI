using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoahStore.Core.Interfaces;
using NoahStore.Infrastructure.Data.DbContexts;
using NoahStore.Infrastructure.Repositories;

namespace NoahStore.Infrastructure
{
    public static class AppServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services
        }
    }
}
