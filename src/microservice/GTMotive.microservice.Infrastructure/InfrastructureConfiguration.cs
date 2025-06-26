using GTMotive.microservice.ApplicationCore.Interfaces;
using GTMotive.microservice.Infrastructure.MongoDb.Settings;
using GTMotive.microservice.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.Infrastructure
{
    /// <summary>
    /// Configures and registers infrastructure services for the application.
    /// </summary>
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            // Register MongoDB settings from configuration
            services.Configure<MongoDbSettings>(options => configuration.GetSection("MongoDb").Bind(options));


            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });
      

            return services;
        }
    }
}
