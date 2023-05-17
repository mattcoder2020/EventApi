using EventAPI.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//write code to read connection string from appsettings.json
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
string connectionstring = config.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EventDbContext>(options => options.UseSqlite(connectionstring));
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
