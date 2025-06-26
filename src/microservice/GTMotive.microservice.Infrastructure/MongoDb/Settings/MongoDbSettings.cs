using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.Infrastructure.MongoDb.Settings
{
    /// <summary>
    /// Represents the configuration settings required to connect to a MongoDB database.
    /// </summary>
    /// <remarks>This class encapsulates the connection string and database name used to establish a
    /// connection to a MongoDB instance. It is typically used to configure MongoDB-related services in an
    /// application.</remarks>
    public class MongoDbSettings
    {
        /// <summary>
        /// Gets or sets the connection string used to establish a connection to the database.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the name of the MongoDB database to be used.
        /// </summary>
        public string MongoDbDatabaseName { get; set; }
    }
}
