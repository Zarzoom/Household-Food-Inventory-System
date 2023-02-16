using InventoryInCSharpAPI.Managers;
using InventoryInCSharpAPI.Services;
using Venflow.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ItemManager>();
// builder.Services.AddDbContext<InventoryRepository>(options =>
//       options.UseMySQL(builder.Configuration.GetValue<string>("ConnectionString")));
builder.Services.AddScoped<PantryManager>();
//builder.Services.AddDatabase<InventoryRepository>((options)=>{options.})
builder.Services.AddDatabase<InventoryRepository>((options) => options.ConnectionString = builder.Configuration.GetValue<string>("ConnectionString"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
