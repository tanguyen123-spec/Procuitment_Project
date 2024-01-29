using Microsoft.EntityFrameworkCore;
using Produ_project.Enitity;
using Produ_project.Repository;
using Produ_project.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var optionsBuilder = new DbContextOptionsBuilder<Produ_projectContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IRepository<>), typeof(MyRepository<>));
builder.Services.AddDbContext<Produ_projectContext>();
builder.Services.AddScoped<IcategoryService, CategoriesService>();
builder.Services.AddScoped<ImainProduct, MainproductService>();
builder.Services.AddScoped<IArtWorkService, ArtWorkService>();
builder.Services.AddScoped<Iquality, QualityService>();
builder.Services.AddScoped<ISuppierInfo, SupplierInfoService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
