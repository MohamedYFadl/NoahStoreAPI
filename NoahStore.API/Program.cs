using Microsoft.EntityFrameworkCore;
using NoahStore.Infrastructure;
using NoahStore.Infrastructure.Data.Config;
using NoahStore.Infrastructure.Data.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var loggerFactory = services.GetRequiredService<ILoggerFactory>();
var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
try
{
    var _dbContext = services.GetRequiredService<ApplicationDbContext>();
    await _dbContext.Database.MigrateAsync();
    await ApplicationDbContextSeed.SeedAsync(_dbContext);

}
catch (Exception ex)
{
    logger.LogError(ex, "An Error has been occured during apply migration");

}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
