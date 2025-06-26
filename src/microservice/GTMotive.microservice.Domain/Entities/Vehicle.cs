using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.Domain.Entities
{
    /// <summary>
    /// Vehicle entity representing a vehicle in the system.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Identifier for the vehicle.
        /// </summary>
        public string Id { get; init; }
        /// <summary>
        /// Brand of the vehicle.
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// Model of the vehicle.
        /// </summary>
        public string Model { get; init; }
        /// <summary>
        /// Manufacture date of the vehicle.
        /// </summary>
        public DateTime ManufactureDate { get; init; }
        /// <summary>
        /// Is the vehicle currently rented?
        /// </summary>
        public bool IsRented { get; private set; }
        /// <summary>
        ///  Identifier of the person who rented the vehicle, if applicable.
        /// </summary>
        public string? RentedBy { get; private set; }

        /// <summary>
        ///  Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        public Vehicle()
        {

        }

        /// <summary>
        /// return a Vehicle instance from persistence data.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <param name="manufactureDate"></param>
        /// <param name="isRented"></param>
        /// <param name="rentedBy"></param>
        /// <returns></returns>
        public static Vehicle FromPersistence(Guid id, string brand, string model, DateTime manufactureDate, bool isRented, string? rentedBy)
        {
            return new Vehicle
            {
                Id = id.ToString(),
                Brand = brand,
                Model = model,
                ManufactureDate = manufactureDate,
                IsRented = isRented,
                RentedBy = rentedBy
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class with the specified model and manufacture date.
        /// </summary>
        /// <param name="model">The model name of the vehicle.</param>
        /// <param name="manufactureDate">The date the vehicle was manufactured. Must not be older than 5 years from the current date.</param>
        /// <exception cref="InvalidOperationException">Thrown if <paramref name="manufactureDate"/> is older than 5 years from the current date.</exception>
        public Vehicle(string brand, string model, DateTime manufactureDate)
        {
            if (manufactureDate < DateTime.UtcNow.AddYears(-5))
                throw new InvalidOperationException("Vehicles older than 5 years are not permitted.");

            Id = Guid.NewGuid().ToString();
            Brand = brand;
            Model = model;
            ManufactureDate = manufactureDate;
            IsRented = false;
        }

        /// <summary>
        /// Rents the vehicle to a person identified by <paramref name="personId"/>.
        /// </summary>
        /// <param name="personId"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Rent(string personId)
        {
            if (IsRented)
                throw new InvalidOperationException("Vehicle is rented");

            IsRented = true;
            RentedBy = personId;
        }

        /// <summary>
        /// Returns the vehicle, making it available for rent again.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void Return()
        {
            if (!IsRented)
                throw new InvalidOperationException("Vehicle is not rented");

            IsRented = false;
            RentedBy = null;
        }
    }
}
