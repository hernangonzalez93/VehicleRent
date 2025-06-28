using GTMotive.microservice.ApplicationCore.Interfaces;
using GTMotive.microservice.ApplicationCore.Ports;
using GTMotive.microservice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.ApplicationCore.Services
{
    /// <summary>
    /// Represents a use case for adding a new vehicle to the system.
    /// </summary>
    /// <remarks>This class provides functionality to create and persist a new vehicle using the specified
    /// repository. It encapsulates the process of vehicle creation and ensures the vehicle is stored in the underlying
    /// data source.</remarks>
    public class AddVehicleUseCase : IAddVehicleUseCase
    {
        private readonly IVehicleRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddVehicleUseCase"/> class.
        /// </summary>
        /// <param name="repository">The repository used to manage vehicle data. This parameter cannot be null.</param>
        public AddVehicleUseCase(IVehicleRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates a new vehicle with the specified model and manufacture date, and adds it to the repository.
        /// </summary>
        /// <remarks>This method asynchronously creates a vehicle and stores it in the repository. The
        /// returned identifier can be used to reference the vehicle in subsequent operations.</remarks>
        ///  <param name="brand"> brand of the vehicle</param>
        /// <param name="model">The model name of the vehicle. Cannot be null or empty.</param>
        /// <param name="manufactureDate">The date the vehicle was manufactured. Must be a valid date.</param>
        /// <returns>A <see cref="string"/> representing the unique identifier of the newly created vehicle.</returns>
        public async Task<string> CreateVehicle(string brand, string model, DateTime manufactureDate)
        {
            var vehicle = new Vehicle(brand, model, manufactureDate);
            await _repository.AddAsync(vehicle);
            return vehicle.Id;
        }
    }
}
