using InventoryInCSharpAPI.Managers;
//using InventoryInCSharpAPI.Services;
using InventoryInCSharpAPI.Repositories;
//using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ItemManager>();
builder.Services.AddSingleton<PantryManager>();
builder.Services.AddSingleton<PantryContentsManager>();
builder.Services.AddSingleton<ItemRepository>();
builder.Services.AddSingleton<PantryRepository>();
builder.Services.AddSingleton<PantryContentsRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI();
// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
