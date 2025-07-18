﻿using GTMotive.microservice.ApplicationCore.Interfaces;
using GTMotive.microservice.ApplicationCore.Ports;
using GTMotive.microservice.ApplicationCore.Services;
using GTMotive.microservice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.test.Integration
{
    /// <summary>
    /// Contains  tests for the <see cref="AddVehicleUseCase"/> class, verifying its behavior and functionality.
    /// </summary>
    public class AddVehicleUseCaseTests
    {
        /// <summary>
        /// Vehicle repository for testing purposes.
        /// </summary>
        public class MockVehicleRepository : IVehicleRepository
        {
            private readonly List<Vehicle> _vehicles = new();

            public Task AddAsync(Vehicle vehicle)
            {
                _vehicles.Add(vehicle);
                return Task.CompletedTask;
            }

            public Task<Vehicle?> GetByIdAsync(string id) =>
                Task.FromResult(_vehicles.FirstOrDefault(v => v.Id == id));

            public Task<List<Vehicle>> ListAsync() => Task.FromResult(_vehicles.ToList());

            public Task UpdateAsync(Vehicle vehicle)
            {
                var index = _vehicles.FindIndex(v => v.Id == vehicle.Id);
                if (index >= 0) _vehicles[index] = vehicle;
                return Task.CompletedTask;
            }

            public Task<bool> HasPersonRentedVehicleAsync(string personId) =>
                Task.FromResult(_vehicles.Any(v => v.RentedBy == personId && v.IsRented));
        }


        /// <summary>
        /// Tests the functionality of adding a vehicle to the fleet using the <see cref="IAddVehicleUseCase"/>
        /// implementation.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Add_Vehicle_To_Fleet()
        {
            var repo = new MockVehicleRepository();
            IAddVehicleUseCase useCase = new AddVehicleUseCase(repo);

            var id = await useCase.CreateVehicle("Ford","Raptor", DateTime.UtcNow.AddYears(-1));
            var vehicle = await repo.GetByIdAsync(id);

            Assert.NotNull(vehicle);
            Assert.Equal("Raptor", vehicle?.Model);
        }
        
    }
}
