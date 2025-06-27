using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.Api
{
    /// <summary>
    /// Provides extension methods for configuring API-related services.
    /// </summary>
    public static class ApiConfiguration
    {
        /// <summary>
        /// Adds services required for the Vehicle API to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the Vehicle API services will be added.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance, allowing for method chaining.</returns>
        public static IServiceCollection AddVehicleApi(this IServiceCollection services)
        {
            return services;
        }
    }
}
