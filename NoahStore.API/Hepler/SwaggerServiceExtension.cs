using Microsoft.OpenApi.Models;

namespace NoahStore.API.Hepler
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",new OpenApiInfo
                {
                    Title = "Noah Store",
                    Version = "v1",
                    
                });

                var sercuriyScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Auhtroization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "bearer",
                        Type = ReferenceType.SecurityScheme,
                    },
                };

                options.AddSecurityDefinition("bearer",sercuriyScheme);

                var securityRequirements = new OpenApiSecurityRequirement
                {
                    { sercuriyScheme, new[] {"bearer"} }
                };

                options.AddSecurityRequirement(securityRequirements);

                

                


            });

            return services;

            
        }
    }
}
