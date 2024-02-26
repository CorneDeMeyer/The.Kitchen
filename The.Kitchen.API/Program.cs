using The.Kitchen.Domain.Models;
using The.Kitchen.Domain.Models.Base;
using The.Kitchen.DomainLogic.Interface;
using The.Kitchen.DomainLogic.Service;

var builder = WebApplication.CreateBuilder(args);
IConfiguration config = builder.Configuration; 

// Add services to the container.
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddSingleton(new RecipeConfig(
    config.GetSection("Recipes").Get<IEnumerable<RecipeBase>>() ?? new List<RecipeBase>()));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
