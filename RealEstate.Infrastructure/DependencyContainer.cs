using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RealEstate.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace RealEstate.Infrastructure
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RealEstateDBConext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("DB_ConnectionString"));
            });

            services.AddTransient<RealEstateDBConext>();

            return services;
        }
    }
}
