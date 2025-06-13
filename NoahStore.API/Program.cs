using Asp.Versioning;
using NoahStore.API.Hepler;
using NoahStore.API.Middleware;
using NoahStore.Core.Dto;
using NoahStore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
var allowedOrigins = builder.Configuration.GetSection("CORSSettings:AllowedOrigins").Get<string[]>();
builder.Services.AddCors(op =>
{
    op.AddPolicy("NoahStorePolicy",builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod()
        .AllowCredentials().WithOrigins(allowedOrigins);
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddServices(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddApiVersioning(static options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1,0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-version"));
}).AddApiExplorer(op =>
{
    op.GroupNameFormat = "'v'V";
    op.SubstituteApiVersionInUrl = true;
});
var app = builder.Build();
app.UseCors("NoahStorePolicy");
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
