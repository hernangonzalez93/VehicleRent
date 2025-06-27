using GTMotive.microservice.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.ApplicationCore.Services
{
    /// <summary>
    /// Handles the process of returning a vehicle by updating its state in the repository.
    /// </summary>
    /// <remarks>This use case retrieves the vehicle by its identifier, updates its state to indicate it has
    /// been returned,  and persists the changes in the repository. If the vehicle is not found, a <see
    /// cref="KeyNotFoundException"/>  is thrown.</remarks>
    public class ReturnVehicleUseCase
    {
        private readonly IVehicleRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleUseCase"/> class.
        /// </summary>
        /// <param name="repository">The repository used to manage vehicle data. This parameter cannot be null.</param>
        public ReturnVehicleUseCase(IVehicleRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Marks a vehicle as returned and updates its status in the repository.
        /// </summary>
        /// <param name="vehicleId">The unique identifier of the vehicle to be returned. Cannot be <see langword="null"/> or empty.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if a vehicle with the specified <paramref name="vehicleId"/> is not found in the repository.</exception>
        public async Task ReturnVehicle(string vehicleId)
        {
            var vehicle = await _repository.GetByIdAsync(vehicleId)
                ?? throw new KeyNotFoundException("Vehicle not found");

            vehicle.Return();
            await _repository.UpdateAsync(vehicle);
        }
    }
}
