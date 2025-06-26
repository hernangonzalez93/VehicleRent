using GTMotive.microservice.Api.Dtos;
using GTMotive.microservice.ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.Api.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehicleController : ControllerBase
    {
        private readonly ILogger<VehicleController> _logger;

        private readonly AddVehicleUseCase _addVehicle;
        private readonly ListVehiclesUseCase _listVehicles;

        public VehicleController(
            AddVehicleUseCase addVehicle,
            ListVehiclesUseCase listVehicles,
            ILogger<VehicleController> logger)
        {
            _addVehicle = addVehicle;
            _listVehicles = listVehicles;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVehicleRequest request)
        {
            _logger.LogInformation("Initialize vehicle creation..");
            try
            {
                var id = await _addVehicle.CreateVehicle(request.Brand ,request.Model, request.ManufactureDate);
                _logger.LogInformation("Created");
                return CreatedAtAction(nameof(GetAll), new { id }, new { id });
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error in Create Vehicle: {ex}");
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting vehicles");
            var vehicles = await _listVehicles.GetVehicles();
            _logger.LogInformation("Returning vehicles");
            return Ok(vehicles);
        }
    }
}
