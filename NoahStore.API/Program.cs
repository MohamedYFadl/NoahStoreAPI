using Microsoft.EntityFrameworkCore;
using NoahStore.API.Hepler;
using NoahStore.API.Middleware;
using NoahStore.Core.Dto;
using NoahStore.Infrastructure;
using NoahStore.Infrastructure.Data.Config;
using NoahStore.Infrastructure.Data.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddServices(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
await SeedUserRoles.SeedUserAndRolesAsync(app);
await ApplySeeding.ApplySeedingAsync(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
