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


builder.Services.AddAutoMapper(typeof(Program));

// Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register Repository
builder.Services.AddScoped<IProductRepository, ProductRepo>();
builder.Services.AddScoped<IUserRepository, UserRepo>();

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


app.MapControllers();

app.Run();

[JsonSerializable(typeof(Product))]
[JsonSerializable(typeof(List<Product>))]
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(List<User>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}