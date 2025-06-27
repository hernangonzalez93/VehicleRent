using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.Api.Dtos
{
    /// <summary>
    /// Represents a request to rent a vehicle, including the identifier of the person making the request.
    /// </summary>

    public class RentVehicleRequest
    {
        public string PersonId { get; set; }
    }
}
