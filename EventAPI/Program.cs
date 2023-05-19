using EventAPI.Infrastructure.Cache;
using EventAPI.Infrastructure.DataAccess;
using EventAPI.Infrastructure.Middleware;
using EventAPI.Infrastructure.Repository;
using EventAPI.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
string connectionstring = config.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<EventDbContext>(options => options.UseSqlite(connectionstring));
builder.Services.AddScoped(typeof(IGenericDbRepository<>), typeof(GenericDbRepository<>));
builder.Services.AddTransient(typeof(IMemoryCache), typeof(CustomMemoryCache));
builder.Services.AddTransient(typeof(IEventService), typeof(EventService));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
