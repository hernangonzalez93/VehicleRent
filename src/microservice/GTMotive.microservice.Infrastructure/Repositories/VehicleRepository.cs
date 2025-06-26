using GTMotive.microservice.ApplicationCore.Interfaces;
using GTMotive.microservice.Domain.Entities;
using GTMotive.microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.microservice.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        public Task AddAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle?> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPersonRentedVehicleAsync(string personId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Vehicle>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
