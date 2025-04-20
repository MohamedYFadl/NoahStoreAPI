using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NoahStore.Core.Interfaces;
using NoahStore.Infrastructure.Data.DbContexts;
using NoahStore.Infrastructure.Repositories;
using NoahStore.API.Errors;
using NoahStore.Core.Services;
using NoahStore.Service;

namespace NoahStore.Infrastructure
{
    public static class AppServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositoy<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IProductService), typeof(ProductService));
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
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

            return services;
        }
    }
}
