using System.Text.Json.Serialization;
using Ecom.InterFaces.Ecom.InterFaces;
using Ecom.Models;
using Ecom.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("No connection string was found");

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register Repository
builder.Services.AddScoped<IProductRepository, ProductRepo>();

// Configure JSON serialization
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Configure the HTTP request pipeline
app.UseHttpsRedirection();
app.UseAuthorization();

// Map controller routes

app.MapControllers();

app.Run();
// Keep this for JSON serialization
[JsonSerializable(typeof(Product))]
[JsonSerializable(typeof(List<Product>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}