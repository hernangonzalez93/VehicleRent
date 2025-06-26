using GTMotive.microservice.Api.Dtos;
using GTMotive.microservice.ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        private readonly AddVehicleUseCase _addVehicle;
        private readonly ListVehiclesUseCase _listVehicles;

        public VehicleController(
            AddVehicleUseCase addVehicle,
            ListVehiclesUseCase listVehicles)
        {
            _addVehicle = addVehicle;
            _listVehicles = listVehicles;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVehicleRequest request)
        {
            try
            {
                var id = await _addVehicle.CreateVehicle(request.Brand ,request.Model, request.ManufactureDate);
                return CreatedAtAction(nameof(GetAll), new { id }, new { id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await _listVehicles.GetVehicles();
            return Ok(vehicles);
        }
    }
}
