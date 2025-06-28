using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.ApplicationCore.Ports
{
    public interface IAddVehicleUseCase
    {
        /// <summary>
        /// Creates a new vehicle with the specified model and manufacture date, and adds it to the repository.
        /// </summary>
        /// <remarks>This method asynchronously creates a vehicle and stores it in the repository. The
        /// returned identifier can be used to reference the vehicle in subsequent operations.</remarks>
        ///  <param name="brand"> brand of the vehicle</param>
        /// <param name="model">The model name of the vehicle. Cannot be null or empty.</param>
        /// <param name="manufactureDate">The date the vehicle was manufactured. Must be a valid date.</param>
        /// <returns>A <see cref="string"/> representing the unique identifier of the newly created vehicle.</returns>
        Task<string> CreateVehicle(string brand, string model, DateTime manufactureDate);
    }
}
