using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.Api.Dtos
{

    /// <summary>
    /// Represents a request to create a new vehicle with specified details.
    /// </summary>
    public class CreateVehicleRequest
    {

        /// <summary>
        /// Gets or sets the brand name of the product.
        /// </summary>
        [Required(ErrorMessage = "Brand is mandatory to add the vehicle")]
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the model name of the item.
        /// </summary>
        [Required(ErrorMessage = "Model is mandatory to add the vehicle")]

        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the date on which the product was manufactured.
        [Required(ErrorMessage ="Manufacture Date is mandatory to add the vehicle")]

        public DateTime ManufactureDate { get; set; }
    }
}
