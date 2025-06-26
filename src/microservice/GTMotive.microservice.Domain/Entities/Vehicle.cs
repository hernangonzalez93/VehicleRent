using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.Domain.Entities
{
    public class Vehicle
    {
        public string Id { get; init; }
        public string Brand { get; set; }
        public string Model { get; init; }
        public DateTime ManufactureDate { get; init; }
        public bool IsRented { get; private set; }
        public string? RentedBy { get; private set; }

        public Vehicle()
        {

        }

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

        public Vehicle(string model, DateTime manufactureDate)
        {
            if (manufactureDate < DateTime.UtcNow.AddYears(-5))
                throw new InvalidOperationException("Vehicles older than 5 years are not permitted.");

            Id = Guid.NewGuid().ToString();
            Model = model;
            ManufactureDate = manufactureDate;
            IsRented = false;
        }

        public void Rent(string personId)
        {
            if (IsRented)
                throw new InvalidOperationException("Vehicle is rented");

            IsRented = true;
            RentedBy = personId;
        }

        public void Return()
        {
            if (!IsRented)
                throw new InvalidOperationException("Vehicle is not rented");

            IsRented = false;
            RentedBy = null;
        }
    }
}
