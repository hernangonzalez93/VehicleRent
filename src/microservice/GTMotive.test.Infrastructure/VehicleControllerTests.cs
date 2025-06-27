using GTMotive.microservice.Api.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GTMotive.test.Infrastructure
{
    /// <summary>
    /// Provides tests for the VehicleController API, focusing on validation, behavior, and response handling.
    /// </summary>
    public class VehicleControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public VehicleControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Should_Validate_Model_On_Create()
        {
            // Arrange
            var client = _factory.CreateClient();

            var request = new CreateVehicleRequest
            {
                Model = "",
                Brand = "Yris", 
                ManufactureDate = DateTime.UtcNow.AddYears(-10) 
            };

            var seri = System.Text.Json.JsonSerializer.Serialize(request);

            //var json = @"{
            //    ""brand"": ""Ford"",
            //    ""manufactureDate"": ""2015-01-01T00:00:00Z""
            //}";
            //var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")

            ///Serialize the request to JSON
            var content = new StringContent(seri, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/vehicles", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var problemDetails = System.Text.Json.JsonSerializer.Deserialize<ValidationProblemDetails>(responseString);

            Assert.True(problemDetails.Errors.ContainsKey("Model"));
            Assert.Contains("Model is mandatory to add the vehicle", problemDetails.Errors["Model"]); //Get into the controller ok
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        public class ValidationProblemDetails
        {
            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("status")]
            public int Status { get; set; }

            [JsonPropertyName("errors")]
            public Dictionary<string, string[]> Errors { get; set; }

        }
    }
}
