using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RealEstate.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace RealEstate.Infrastructure
{
    /// <summary>
    /// Extension method to register services in the dependency injection container.
    /// </summary>
    public static class DependencyContainer
    {
        /// <summary>
        /// Registers the required services for the RealEstateAPI.
        /// </summary>
        /// <param name="services">The IServiceCollection instance to add the services to.</param>
        /// <param name="configuration">The IConfiguration instance containing configuration data.</param>
        /// <returns>The modified IServiceCollection instance.</returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure the RealEstateDBConext with the specified connection string
            services.AddDbContext<RealEstateDBContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("DB_ConnectionString"));
            });

            services.AddTransient<RealEstateDBContext>();

            return services;
        }
    }
}
