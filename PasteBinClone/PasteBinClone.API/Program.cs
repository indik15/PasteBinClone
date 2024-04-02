using Microsoft.EntityFrameworkCore;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Domain.Models;
using PasteBinClone.Persistence.Data;
using PasteBinClone.Persistence.Repository;
using AutoMapper;
using System.Reflection;
using PasteBinClone.Application.Services;
using PasteBinClone.Application.Mappings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(MappingConfiguration).Assembly);

builder.Services.AddScoped<IBaseRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSerilog();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "An error occurred while starting the application.");
}