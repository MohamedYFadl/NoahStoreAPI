using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NoahStore.Core.Interfaces;
using NoahStore.Infrastructure.Data.DbContexts;
using NoahStore.Infrastructure.Repositories;
using NoahStore.API.Errors;
using NoahStore.Core.Services;
using NoahStore.Service;
using StackExchange.Redis;

namespace NoahStore.Infrastructure
{
    public static class AppServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
        {
            #region Register services
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositoy<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IProductService), typeof(ProductService)); 
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository)); 
            #endregion
            #region DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                });
            #endregion
            #region Apply Validation Error
            services.Configure<ApiBehaviorOptions>(op =>
                {
                    op.InvalidModelStateResponseFactory = (context) =>
                    {
                        var errors = context.ModelState.Where(e => e.Value.Errors.Count() > 0)
                         .SelectMany(p => p.Value.Errors).Select(e => e.ErrorMessage).ToList();

                        var response = new APIValidationErrorResponse()
                        {
                            Errors = errors
                        };
                        return new BadRequestObjectResult(response);
                    };
                });
            #endregion
            #region Apply Redis Cashing
            services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var config = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(config);
            });
            #endregion
            return services;
        }
    }
}
