using InventoryInCSharpAPI.Managers;
//using InventoryInCSharpAPI.Services;
using InventoryInCSharpAPI.Repositories;
using InventoryInCSharpAPI.Models;
//using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);//
var password = builder.Configuration.GetValue<string>("password");//
var connection = $"{builder.Configuration.GetValue<string>("ConnectionString")}password={password};";//
var CSSOS = new ConnectionStringAndOtherSecrets(connection);
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
builder.Services.AddSingleton<ConnectionStringAndOtherSecrets>(CSSOS);

Console.WriteLine(connection);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
  
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins("http://localhost:3000",
                "http://localhost:8000");
        });
});  
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());
app.Run();
