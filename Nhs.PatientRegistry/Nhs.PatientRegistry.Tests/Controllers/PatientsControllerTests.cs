using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Nhs.PatientRegistry.Api.Controllers;
using Nhs.PatientRegistry.Api.DTOs;
using Nhs.PatientRegistry.Api.Services;
using Nhs.PatientRegistry.Api.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nhs.PatientRegistry.Tests.Controllers
{
    public class PatientsControllerTests
    {
        private readonly Mock<IPatientService> _patientServiceMock;
        private readonly Mock<ILogger<PatientsController>> _loggerMock;
        private readonly IValidator<int> _validator;
        private readonly PatientsController _controller;

        public PatientsControllerTests()
        {
            _validator = new PatientIdValidator();
            _patientServiceMock = new Mock<IPatientService>();
            _loggerMock = new Mock<ILogger<PatientsController>>();
            _controller = new PatientsController(_patientServiceMock.Object, _validator, _loggerMock.Object);
            
        }

        [Fact]
        public async Task GetPatientDetails_WhenPatientExists_ReturnsResultOk()
        {
            // arrange
            var patientDetails = new PatientDetailsDto
            {
                Id = 1,
                NHSNumber = "9108286498",
                Name = "Michael Richard",
                DateOfBirth = new DateTime(1985, 4, 12),
                GPPractice = "Delapre Medical Centre Northampton",
            };

            _patientServiceMock.Setup(service => service.GetPatientDetailsByIdAsync(1)).ReturnsAsync(patientDetails);

            // act
            var result = await _controller.GetPatientDetailsById(1);

            // assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(patientDetails);
        }

        [Fact]
        public async Task GetPatientDetails_WhenPatientDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _patientServiceMock.Setup(service => service.GetPatientDetailsByIdAsync(999)).ReturnsAsync((PatientDetailsDto?)null);

            // Act
            var result = await _controller.GetPatientDetailsById(999);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetPatientDetails_WhenPatientIdIsInvalid_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.GetPatientDetailsById(0);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

    }
}
