using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Nhs.PatientRegistry.Api.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace Nhs.PatientRegistry.Tests.Integration
{
    public class PatientsApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PatientsApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetPatientById_WhenPatientExist_ReturnsOkWithDetails()
        {
            //act
            var response = await _client.GetAsync("/api/v1/patients/1");

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var patient = await response.Content.ReadFromJsonAsync<PatientDetailsDto>();

            patient.Should().NotBeNull();
            patient!.Id.Should().Be(1);
            patient.Name.Should().Be("John Smith");
            patient.NHSNumber.Should().Be("4857773456");
            patient.GPPractice.Should().Be("Delapre Medical Centre Northampton");

        }


        [Fact]
        public async Task GetPatientById_WhenPatientDoesNotExist_ReturnsNotFound()
        {
            //act
            var response = await _client.GetAsync("/api/v1/patients/123");

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
            error.Should().NotBeNull();
            error!.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            error.Message.Should().Be("Patient with ID 123 was not found.");
        }
        [Fact]
        public async Task GetPatientById_WhenPatientIdIsInvalid_ReturnsBadRequest()
        {

            //act
            var response = await _client.GetAsync("/api/v1/patients/-1");

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var apiError = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
            apiError.Should().NotBeNull();
            apiError!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            apiError.Message.Should().Be("Patient ID must be a positive integer.");


        }
    }

}
