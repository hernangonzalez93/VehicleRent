using GTMotive.microservice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.ApplicationCore.Ports
{
    public interface IListVehiclesUseCase
    {
        /// <summary>
        /// Retrieves a list of all vehicles.
        /// </summary>
        /// <remarks>This method asynchronously fetches all vehicles from the underlying data repository.
        /// The returned list may be empty if no vehicles are available.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of  <see cref="Vehicle"/>
        /// objects representing the vehicles in the repository.</returns>
        Task<List<Vehicle>> GetVehicles();
    }
}
