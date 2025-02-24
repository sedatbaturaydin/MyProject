using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // Add this using directive
using MyProject.Application.Extensions;
using MyProject.Application.Services;
using MyProject.Core.Interfaces;
using MyProject.Infrastructure.Data;
using MyProject.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.Swagger; // Add this using directive
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// OpenAPI (Swagger) servislerini ekleyelim
builder.Services.AddEndpointsApiExplorer(); // Swagger i�in gerekli
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();
builder.Services.AddApplicationServices();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

var app = builder.Build();

// Middleware ekleyelim
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Hata ay�klama i�in
    app.UseSwagger();                // Swagger UI i�in gerekli
    app.UseSwaggerUI();              // Swagger UI i�in gerekli

    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization(); // E�er Authentication ekleyeceksen

app.MapControllers(); // API endpoint'lerini ekleyelim

app.Run();