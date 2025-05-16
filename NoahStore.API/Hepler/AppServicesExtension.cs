using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NoahStore.Core.Interfaces;
using NoahStore.Infrastructure.Data.DbContexts;
using NoahStore.Infrastructure.Repositories;
using NoahStore.API.Errors;
using NoahStore.Core.Services;
using NoahStore.Service;
using StackExchange.Redis;
using NoahStore.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NoahStore.API.Hepler;
using NoahStore.API.Mapping;

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
            services.AddScoped(typeof(IMailService), typeof(MailService));
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.AddScoped(typeof(IAuthRepository), typeof(AuthRepository));
            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            #endregion
            #region DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                });
            #endregion

            #region Token Config
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

            }).AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                
                
            }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Token:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["Token:Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Token:SecretKey"])),
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromDays(5)
                    };
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
