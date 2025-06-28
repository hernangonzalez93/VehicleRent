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
    /// Provides functionality to retrieve a list of vehicles from the underlying data source.
    /// </summary>
    /// <remarks>This use case interacts with an <see cref="IVehicleRepository"/> to fetch vehicle data. It is
    /// designed to encapsulate the logic for listing vehicles, ensuring separation of concerns.</remarks>
    public class ListVehiclesUseCase : IListVehiclesUseCase
    {
        private readonly IVehicleRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListVehiclesUseCase"/> class.
        /// </summary>
        /// <param name="repository">The repository used to retrieve vehicle data. This parameter cannot be null.</param>
        public ListVehiclesUseCase(IVehicleRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves a list of all vehicles.
        /// </summary>
        /// <remarks>This method asynchronously fetches all vehicles from the underlying data repository.
        /// The returned list may be empty if no vehicles are available.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of  <see cref="Vehicle"/>
        /// objects representing the vehicles in the repository.</returns>
        public async Task<List<Vehicle>> GetVehicles()
        {
            return await _repository.ListAsync();
        }
    }
}
