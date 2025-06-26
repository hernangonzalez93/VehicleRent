using GTMotive.microservice.ApplicationCore.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.ApplicationCore
{
    /// <summary>
    /// Provides configuration methods for registering core application services related to vehicle management.
    /// </summary>
    public static class ApplicationCoreConfiguration
    {
        /// <summary>
        /// Registers the core application services required for vehicle management use cases.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the services will be added.</param>
        /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
        public static IServiceCollection AddApplicationCore(this IServiceCollection services)
        {
            // ad application core services for vehicle management use cases
            services.AddScoped<AddVehicleUseCase>();
            services.AddScoped<ListVehiclesUseCase>();
            services.AddScoped<RentVehicleUseCase>();
            services.AddScoped<ReturnVehicleUseCase>();
            return services;
        }
    }
}
