using AirportRenovate.Server.Datas;
using Microsoft.EntityFrameworkCore;
using AirportRenovate.Server.Filters;
//using System.Text.Json.Serialization;
using AirportRenovate.Server.Mappings;
using System.Text.Json.Serialization;
using AirportRenovate.Server.Utilities;
using AirportRenovate.Server.Interfaces.Repositorys;
using MyGisMIS.Server.Repositorys;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new CustomExceptionFilterAttribute());
    options.Filters.Add(new CustomResultFilterAttribute());
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.MaxDepth = 64;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    //options.JsonSerializerOptions.Converters.Add(new JsonConverterUtility.DateTimeJsonConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(ViewModelMapping));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<DESEncryptionUtility>();

builder.Services.AddDbContext<AirportBudgetDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("Money")));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
