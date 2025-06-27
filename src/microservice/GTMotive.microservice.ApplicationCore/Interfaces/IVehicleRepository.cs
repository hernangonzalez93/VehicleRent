using GTMotive.microservice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.ApplicationCore.Interfaces
{
    /// <summary>
    /// Defines the contract for a repository that manages vehicle entitiy. 
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Add a car to the inventory
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        Task AddAsync(Vehicle vehicle);

        /// <summary>
        /// GeT A Car by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Vehicle?> GetByIdAsync(string id);

        /// <summary>
        /// List all vehicles in the inventory
        /// </summary>
        /// <returns></returns>
        Task<List<Vehicle>> ListAsync();

        /// <summary>
        /// Update a vehicle in the inventory
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        Task UpdateAsync(Vehicle vehicle);

        /// <summary>
        /// Determines whether the specified person has rented a vehicle.
        /// </summary>
        /// <param name="personId">The unique identifier of the person to check. Cannot be <see langword="null"/> or empty.</param>
        /// <returns><see langword="true"/> if the person has rented a vehicle; otherwise, <see langword="false"/>.</returns>
        Task<bool> HasPersonRentedVehicleAsync(string personId);
    }
}
