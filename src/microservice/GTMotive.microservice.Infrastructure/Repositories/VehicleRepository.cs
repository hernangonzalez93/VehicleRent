using GTMotive.microservice.ApplicationCore.Interfaces;
using GTMotive.microservice.Domain.Entities;
using GTMotive.microservice.Infrastructure.MongoDb.Documents;
using GTMotive.microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.Infrastructure.Repositories
{
    /// <summary>
    /// Provides methods for managing vehicle data in MongoDB database.
    /// </summary>
    /// <remarks>This repository is responsible for performing operations on vehicle data,</remarks>
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IMongoCollection<VehicleDocument> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleRepository"/> class, providing access to the vehicle
        /// collection in the MongoDB database.
        /// </summary>
        /// <remarks>This constructor sets up the repository by connecting to the specified MongoDB
        /// database and initializing the vehicle collection.</remarks>
        /// <param name="client">The <see cref="IMongoClient"/> used to connect to the MongoDB server.</param>
        /// <param name="options">The configuration options containing the MongoDB database name.</param>
        public VehicleRepository(IMongoClient client, IOptions<MongoDbSettings> options)
        {
            var database = client.GetDatabase(options.Value.MongoDbDatabaseName);
            _collection = database.GetCollection<VehicleDocument>("vehicles");
        }

        /// <summary>
        /// Asynchronously adds a new vehicle to the database.
        /// </summary>
        /// <param name="vehicle">The vehicle to add. Cannot be <see langword="null"/>.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddAsync(Vehicle vehicle)
        {
            var doc = VehicleDocument.FromDomain(vehicle);
            await _collection.InsertOneAsync(doc);
        }

        /// <summary>
        /// Retrieves a vehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle to retrieve. Cannot be null or empty.</param>
        /// <returns>A <see cref="Vehicle"/> object representing the vehicle with the specified identifier,  or <see
        /// langword="null"/> if no matching vehicle is found.</returns>
        public async Task<Vehicle?> GetByIdAsync(string id)
        {
            var doc = await _collection.Find(v => v.Id == id).FirstOrDefaultAsync();
            return doc?.ToDomain();
        }

        /// <summary>
        /// Retrieves a list of vehicles from the data source.
        /// </summary>
        /// <remarks>This method asynchronously fetches all vehicle records from the underlying collection
        /// and converts them to domain objects. The returned list will be empty if no vehicles are found.</remarks>
        /// <returns>A task representing the asynchronous operation. The task result contains a list of  <see cref="Vehicle"/>
        /// objects representing the vehicles in the data source.</returns>
        public async Task<List<Vehicle>> ListAsync()
        {
            var docs = await _collection.Find(_ => true).ToListAsync();
            return docs.Select(d => d.ToDomain()).ToList();
        }

        /// <summary>
        /// Updates the specified vehicle in the database asynchronously.
        /// </summary>
        /// <remarks>This method replaces the existing database record that matches the <see
        /// cref="Vehicle.Id"/> of the provided <paramref name="vehicle"/>. Ensure that the <paramref name="vehicle"/>
        /// object contains valid data before calling this method.</remarks>
        /// <param name="vehicle">The vehicle to update. The <see cref="Vehicle.Id"/> property must match an existing record in the database.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        public async Task UpdateAsync(Vehicle vehicle)
        {
            var doc = VehicleDocument.FromDomain(vehicle);
            await _collection.ReplaceOneAsync(v => v.Id == vehicle.Id, doc);
        }

        /// <summary>
        /// Determines whether the specified person has rented a vehicle.
        /// </summary>
        /// <remarks>This method queries the underlying data source to check if any vehicle is currently
        /// rented by the specified person.</remarks>
        /// <param name="personId">The unique identifier of the person to check. Cannot be null or empty.</param>
        /// <returns><see langword="true"/> if the person has rented at least one vehicle; otherwise, <see langword="false"/>.</returns>
        public async Task<bool> HasPersonRentedVehicleAsync(string personId)
        {
            return await _collection.Find(v => v.RentedBy == personId && v.IsRented).AnyAsync();
        }
    }
}
