using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.API.Middlewares;
using RealEstate.API.Models;
using RealEstate.Application.Interfaces;
using RealEstate.Application.Services;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure;
using RealEstate.Infrastructure.Context;
using RealEstate.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
                
            return new BadRequestObjectResult(new ErrorResponse
            {
                Message = "Model validation error",
                Errors = errors
            });
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
    var context = scope.ServiceProvider.GetRequiredService<RealEstateDBContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.UseExceptionMiddleware();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
