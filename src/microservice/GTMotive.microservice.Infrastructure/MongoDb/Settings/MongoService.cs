using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.Infrastructure.MongoDb.Settings
{
    public static class MongoService
    {
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(options => configuration.GetSection("MongoDb").Bind(options));
            var settings = configuration.GetSection("MongoDb").Get<MongoDbSettings>();
            if (settings == null || string.IsNullOrEmpty(settings.ConnectionString) || string.IsNullOrEmpty(settings.MongoDbDatabaseName))
            {
                throw new ArgumentException("MongoDbSettings are not properly configured in the appsettings.json file.");
            }
            services.AddSingleton<IMongoClient>(sp => new MongoClient(settings.ConnectionString));
            }
        }
}
