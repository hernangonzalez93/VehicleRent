using GTMotive.microservice.Domain.Entities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.Infrastructure.MongoDb.Documents
{
    /// <summary>
    /// Represents a document that stores information about a vehicle in a persistence layer.
    /// </summary>
    /// <remarks>This class is designed to map vehicle data for storage and retrieval in a database
    /// cref="Vehicle"/> and the persistence model <see cref="VehicleDocument"/>.</remarks>
    public  class VehicleDocument
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        [BsonId]
        public string Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the brand name of the product.
        /// </summary>
        public string Brand { get; set; } = default!;

        /// <summary>
        /// Gets or sets the model name associated with the object.
        /// </summary>
        public string Model { get; set; } = default!;

        /// <summary>
        /// Gets or sets the date the product was manufactured.
        /// </summary>
        public DateTime ManufactureDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is currently rented.
        /// </summary>
        public bool IsRented { get; set; }

        /// <summary>
        /// Gets or sets the name of the individual or entity that has rented the item.
        /// </summary>
        public string? RentedBy { get; set; }


        /// <summary>
        /// Converts a <see cref="Vehicle"/> domain object into a <see cref="VehicleDocument"/> data transfer object.
        /// </summary>
        /// <param name="vehicle">The <see cref="Vehicle"/> instance to convert. Cannot be <see langword="null"/>.</param>
        /// <returns>A <see cref="VehicleDocument"/> instance containing the mapped data from the provided <see cref="Vehicle"/>
        /// object.</returns>
        public static VehicleDocument FromDomain(Vehicle vehicle) => new()
        {
            Id = vehicle.Id,
            Model = vehicle.Model,
            Brand = vehicle.Brand,
            ManufactureDate = vehicle.ManufactureDate,
            IsRented = vehicle.IsRented,
            RentedBy = vehicle.RentedBy
        };

        /// <summary>
        /// Converts the current persistence model representation of a vehicle into its domain model equivalent.
        /// </summary>
        /// <remarks>This method maps the properties of the persistence model to the corresponding
        /// properties of the domain model. Ensure that the persistence model contains valid data before calling this
        /// method.</remarks>
        /// <returns>A <see cref="Vehicle"/> object representing the domain model of the vehicle.</returns>
        public Vehicle ToDomain()
        {
            return Vehicle.FromPersistence(
                new Guid(Id),
                Brand,
                Model,
                ManufactureDate,
                IsRented,
                RentedBy
            );
        }
    }
}
