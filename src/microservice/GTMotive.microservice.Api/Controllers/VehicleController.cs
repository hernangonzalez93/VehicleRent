using GTMotive.microservice.Api.Dtos;
using GTMotive.microservice.ApplicationCore.Ports;
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
        
        private readonly IAddVehicleUseCase _addVehicle;
        private readonly IListVehiclesUseCase _listVehicles;
        private readonly IRentVehicleUseCase _rentVehicle;
        private readonly IReturnVehicleUseCase _returnVehicle;
        private readonly ILogger<VehicleController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleController"/> class, providing functionality  for
        /// managing vehicle operations such as adding, listing, renting, and returning vehicles.
        /// </summary>
        /// <param name="addVehicle">The use case for adding a new vehicle to the system.</param>
        /// <param name="listVehicles">The use case for retrieving a list of available vehicles.</param>
        /// <param name="rentVehicle">The use case for renting a vehicle.</param>
        /// <param name="returnVehicle">The use case for returning a rented vehicle.</param>
        /// <param name="logger">The logger instance used for logging operations and diagnostics.</param>
        public VehicleController(
            IAddVehicleUseCase addVehicle,
            IListVehiclesUseCase listVehicles,
            IRentVehicleUseCase rentVehicle,
            IReturnVehicleUseCase returnVehicle,
            ILogger<VehicleController> logger)
        {
            _addVehicle = addVehicle;
            _listVehicles = listVehicles;
            _rentVehicle = rentVehicle;
            _returnVehicle = returnVehicle;
            _logger = logger;
        }

        /// <summary>
        ///  Creates a new vehicle with the specified details and adds it to the system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets all vehicles available in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting vehicles");
            var vehicles = await _listVehicles.GetVehicles();
            _logger.LogInformation("Returning vehicles");
            return Ok(vehicles);
        }


        /// <summary>
        ///  Rents a vehicle to a person identified by their ID.
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{vehicleId}/rent")]
        [Authorize]
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

        /// <summary>
        /// Returns a rented vehicle identified by its ID.
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
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
