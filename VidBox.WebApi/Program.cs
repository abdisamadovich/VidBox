using VidBox.DataAccess.Interfaces.Categories;
using VidBox.DataAccess.Repositories.Categories;
using VidBox.Service.Interfaces.Categories;
using VidBox.Service.Interfaces.Common;
using VidBox.Service.Services.Categories;
using VidBox.Service.Services.Commons;
using VidBox.WebApi.Middlewares;
using ExceptionHandlerMiddleware = Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPaginator, Paginator>();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseStaticFiles();
app.UseMiddleware<CrosOriginAccessMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
