using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Application.Interfaces;
using RealEstate.Application.Services;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure;
using RealEstate.Infrastructure.Context;
using RealEstate.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services and repository
builder.Services.AddTransient<IBuildingPropertyService, BuildingPropertyService>();
builder.Services.AddTransient<IBuildingPropertyRepository, BuildingPropertyRepository>();

builder.Services.AddTransient<IOwnerService, OwnerService>();
builder.Services.AddTransient<IOwnerRepository, OwnerRepository>();

// Register database services
builder.Services.RegisterServices(builder.Configuration);


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );
});

var app = builder.Build();

// Create database migrations
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RealEstateDBConext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
