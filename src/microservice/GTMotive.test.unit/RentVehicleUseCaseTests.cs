using GTMotive.microservice.ApplicationCore.Interfaces;
using GTMotive.microservice.ApplicationCore.Services;
using GTMotive.microservice.Domain.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTMotive.test.unit
{
    /// <summary>
    /// Contains unit tests for the <see cref="RentVehicleUseCase"/> class.
    /// </summary>
    public class RentVehicleUseCaseTests
    {

        [Fact]
        public async Task Should_Throw_If_Person_Already_Has_Vehicle()
        {
            var repo = new Mock<IVehicleRepository>();
            repo.Setup(r => r.HasPersonRentedVehicleAsync("user1")).ReturnsAsync(true);

            var useCase = new RentVehicleUseCase(repo.Object);

            await Assert.ThrowsAsync<BusinessRuleViolationException>(() =>
                useCase.DoRent("v1", "user1"));
        }
    }
}
