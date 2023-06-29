using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RealEstate.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RealEstate.Infrastructure.ExternalServices.Storage.Azure;
using RealEstate.Infrastructure.ExternalServices.Storage;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Configure the RealEstateDBConext with the specified connection string
            services.AddDbContext<RealEstateDBContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("DB_ConnectionString"));
            });

            services.AddTransient<RealEstateDBContext>();
            services.AddSingleton<IStorageService, AzureStorageService>(provider =>
            {
                var storageConnectionString = Environment.GetEnvironmentVariable("Azure_StorageConnectionString");
                return new AzureStorageService(storageConnectionString);
            });

            return services;
        }
    }
}
