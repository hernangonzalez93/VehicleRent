using GTMotive.microservice.Api.Dtos;
using GTMotive.microservice.ApplicationCore.Services;
using GTMotive.microservice.Domain.Exceptions;
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
        private readonly RentVehicleUseCase _rentVehicle;
        private readonly ReturnVehicleUseCase _returnVehicle;

        public VehicleController(
            AddVehicleUseCase addVehicle,
            ListVehiclesUseCase listVehicles,
            RentVehicleUseCase rentVehicle,
            ReturnVehicleUseCase returnVehicle,
            ILogger<VehicleController> logger)
        {
            _addVehicle = addVehicle;
            _listVehicles = listVehicles;
            _rentVehicle = rentVehicle;
            _returnVehicle = returnVehicle;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVehicleRequest request)
        {
            _logger.LogInformation("Initialize vehicle creation..");
            try
            {
                // Validate request model
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("Invalid model state");
                    return BadRequest(ModelState);
                }

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
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting vehicles");
            var vehicles = await _listVehicles.GetVehicles();
            _logger.LogInformation("Returning vehicles");
            return Ok(vehicles);
        }

        [HttpPost("{vehicleId}/rent")]
        public async Task<IActionResult> Rent(string vehicleId, [FromBody] RentVehicleRequest request)
        {
            _logger.LogInformation($"Renting vehicle {vehicleId} for person {request.PersonId}");
            try
            {    
                await _rentVehicle.DoRent(vehicleId, request.PersonId);
                return NoContent();
            }
            catch (BusinessRuleViolationException ex)
            {
                _logger.LogInformation($"Business rule violation: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException)
            {
                _logger.LogInformation($"Vehicle with ID {vehicleId} not found.");
                return NotFound(new { message = "Vehicle not found.." });
            }
        }

        [HttpPost("{vehicleId}/return")]
        public async Task<IActionResult> Return(string vehicleId)
        {
            _logger.LogInformation($"Returning vehicle {vehicleId}");
            try
            {
                await _returnVehicle.ReturnVehicle(vehicleId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                _logger.LogInformation($"Vehicle with ID {vehicleId} not found.");
                return NotFound(new { message = "Vehicle not found.." });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, new { message = ex.Message });
            }
        }
    }
}
