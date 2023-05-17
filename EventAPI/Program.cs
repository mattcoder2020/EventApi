using EventAPI.Infrastructure.DB;
using EventAPI.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
string connectionstring = config.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<EventDbContext>(options => options.UseSqlite(connectionstring));
//add a services.AddSingleton for DI for IGenericDbRepository
builder.Services.AddScoped(typeof(IGenericDbRepository<>), (typeof(GenericDbRepository<>)));

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
