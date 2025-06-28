using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.ApplicationCore.Ports
{
    public interface IReturnVehicleUseCase
    {
        /// <summary>
        /// Marks a vehicle as returned and updates its status in the repository.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle to be returned. Cannot be <see langword="null"/> or empty.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if a vehicle with the specified <paramref name="vehicleId"/> is not found in the repository.</exception>
        Task ReturnVehicle(string vehicleId);
    }
}
