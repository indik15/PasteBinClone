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
using FluentValidation;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Dto.Validations;
using PasteBinClone.API.ExceptionHandler;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(MappingConfiguration).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IBaseRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IBaseRepository<ContentType>, ContentTypeRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IContentTypeService, ContentTypeService>();

builder.Services.AddScoped<IValidator<CategoryDto>, CategoryDtoValidator>();
builder.Services.AddScoped<IValidator<ContentTypeDto>, ContentTypeDtoValidator>();

builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSerilog();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

var app = builder.Build();

app.UseExceptionHandler();

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