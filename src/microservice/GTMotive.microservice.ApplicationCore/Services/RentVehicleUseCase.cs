using GTMotive.microservice.ApplicationCore.Interfaces;
using GTMotive.microservice.ApplicationCore.Ports;
using GTMotive.microservice.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.ApplicationCore.Services
{
    /// <summary>
    /// Handles the process of renting a vehicle by a person.
    /// </summary>
    /// <remarks>This use case ensures that a person can rent a vehicle only if they do not already have a
    /// vehicle rented. It retrieves the specified vehicle, validates its availability, and updates its state to reflect
    /// the rental.</remarks>
    public class RentVehicleUseCase : IRentVehicleUseCase
    {
        private readonly IVehicleRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleUseCase"/> class.
        /// </summary>
        /// <param name="repository">The repository used to manage vehicle data. This parameter cannot be null.</param>
        public RentVehicleUseCase(IVehicleRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Initiates the rental process for a vehicle by a specified person.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle to be rented. Cannot be null or empty.</param>
        /// <param name="personId">The unique identifier of the person renting the vehicle. Cannot be null or empty.</param>
        /// <returns></returns>
        /// <exception cref="BusinessRuleViolationException">Thrown if the specified person has already rented a vehicle.</exception>
        /// <exception cref="KeyNotFoundException">Thrown if the vehicle with the specified <paramref name="vehicleId"/> does not exist.</exception>
        public async Task DoRent(string vehicleId, string personId)
        {
            if (await _repository.HasPersonRentedVehicleAsync(personId))
                throw new BusinessRuleViolationException("Person already has vehicle rented");

            var vehicle = await _repository.GetByIdAsync(vehicleId)
                ?? throw new KeyNotFoundException("Vehicle not found");

            //rent the vehicle
            vehicle.Rent(personId);
            await _repository.UpdateAsync(vehicle);
        }
    }
}
