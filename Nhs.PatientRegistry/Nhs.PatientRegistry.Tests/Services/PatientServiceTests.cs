using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Nhs.PatientRegistry.Api.Abstractions;
using Nhs.PatientRegistry.Api.Mapping;
using Nhs.PatientRegistry.Api.Models;
using Nhs.PatientRegistry.Api.Services;

namespace Nhs.PatientRegistry.Tests.Services
{
    public class PatientServiceTests
    {
        private readonly Mock<IPatientRepository> _patientRepositoryMock;
        private readonly PatientService _patientService;
        private readonly Mock<ILogger<PatientService>> _loggerMock;
        private readonly IMapper _mapper;
        public PatientServiceTests()
        {
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _loggerMock = new Mock<ILogger<PatientService>>();

            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<PatientMappingProfile>();
            }, NullLoggerFactory.Instance);
            _mapper = mapperConfiguration.CreateMapper();

            _patientService = new PatientService(_loggerMock.Object, _patientRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetPatientDetailsByIdAsync_WhenPatientExists_ReturnsPatientDetails()
        {
            // arrange
            var patient = new Patient
            {
                Id = 1,
                NHSNumber = "9108286498",
                Name = "Amelia Carter",
                DateOfBirth = new DateTime(1982, 12, 25),
                GPPractice = "Delapre Medical Centre Northampton"
            };

            _patientRepositoryMock.Setup(source => source.GetPatientByIdAsync(1)).ReturnsAsync(patient);


            // act
            var result = await _patientService.GetPatientDetailsByIdAsync(1);

            // assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(1);
            result.NHSNumber.Should().Be("9108286498");
            result.Name.Should().Be("Amelia Carter");
            result.GPPractice.Should().Be("Delapre Medical Centre Northampton");

        }

        [Fact]
        public async Task GetPatientDetailsByIdAsync_WhenPatientDoesNotExist_ReturnsNull()
        {

            _patientRepositoryMock.Setup(source => source.GetPatientByIdAsync(255)).ReturnsAsync((Patient?)null);

            var result = await _patientService.GetPatientDetailsByIdAsync(255);

            result.Should().BeNull();
        }
    }
}
