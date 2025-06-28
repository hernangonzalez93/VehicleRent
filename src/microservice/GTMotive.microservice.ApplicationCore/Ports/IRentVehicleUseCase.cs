using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.ApplicationCore.Ports
{
    public interface IRentVehicleUseCase
    {
        /// <summary>
        /// Initiates the rental process for a vehicle by a specified person.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle to be rented. Cannot be null or empty.</param>
        /// <param name="personId">The unique identifier of the person renting the vehicle. Cannot be null or empty.</param>
        /// <returns></returns>
        /// <exception cref="BusinessRuleViolationException">Thrown if the specified person has already rented a vehicle.</exception>
        /// <exception cref="KeyNotFoundException">Thrown if the vehicle with the specified <paramref name="vehicleId"/> does not exist.</exception>
        Task DoRent(string vehicleId, string personId);
    }
}
