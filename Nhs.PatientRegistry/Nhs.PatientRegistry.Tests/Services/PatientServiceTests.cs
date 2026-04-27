using FluentAssertions;
using Moq;
using Nhs.PatientRegistry.Api.Abstractions;
using Nhs.PatientRegistry.Api.Models;
using Nhs.PatientRegistry.Api.Services;

namespace Nhs.PatientRegistry.Tests.Services
{
    public class PatientServiceTests
    {
        private readonly Mock<IPatientRepository> _patientRepositoryMock;
        private readonly PatientService _patientService;

        public PatientServiceTests()
        {
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _patientService = new PatientService(_patientRepositoryMock.Object);
        }

        [Fact]
        public async Task GetPatientDetailsByIdAsync_WhenPatientExists_ReturnsPatientDetails()
        {
            // arrange
            var patients = new List<Patient>
                {
                    new Patient
                        {
                            Id = 1,
                            NHSNumber = "9108286498",
                            Name = "Rahul Kokkaranikkal",
                            DateOfBirth = new DateTime(1982, 12, 25),
                            GPPractice = "Delapre Medical Centre Northampton"
                        }
                };

            _patientRepositoryMock.Setup(source => source.GetPatientsAsync()).ReturnsAsync(patients);

            // act
            var result = await _patientService.GetPatientDetailsByIdAsync(1);

            // assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(1);
            result.NHSNumber.Should().Be("9108286498");
            result.Name.Should().Be("Rahul Kokkaranikkal");
            result.GPPractice.Should().Be("Delapre Medical Centre Northampton");

        }

        [Fact]
        public async Task GetPatientDetailsByidAsync_WhenPatientDoesnotExist_ReturnsNull()
        {
            
            var patients = new List<Patient>
                {
                    new Patient
                    {
                        Id = 25,
                        NHSNumber = "9108286498",
                        Name = "Rahul Kokkaranikkal",
                        DateOfBirth = new DateTime(1982, 12, 25),
                        GPPractice = "Delapre Medical Centre Northampton"
                    }
                };

            _patientRepositoryMock.Setup(source => source.GetPatientsAsync()).ReturnsAsync(patients);
           
            var result = await _patientService.GetPatientDetailsByIdAsync(999);
           
            result.Should().BeNull();
        }
    }
}
