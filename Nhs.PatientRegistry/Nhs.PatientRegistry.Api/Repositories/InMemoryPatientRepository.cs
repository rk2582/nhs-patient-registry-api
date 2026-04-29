using Nhs.PatientRegistry.Api.Abstractions;
using Nhs.PatientRegistry.Api.Models;

namespace Nhs.PatientRegistry.Api.Repositories
{
    public class InMemoryPatientRepository : IPatientRepository
    {
        private readonly ILogger<InMemoryPatientRepository> _logger;
        public InMemoryPatientRepository(ILogger<InMemoryPatientRepository> logger)
        {
            _logger = logger;
        }
        private static readonly Patient[] Patients =
                {
                    new Patient
                    {
                        Id = 1,
                        NHSNumber = "4857773456",
                        Name = "John Smith",
                        DateOfBirth = new DateTime(1985, 4, 12),
                        GPPractice = "Delapre Medical Centre Northampton"
                    },
                    new Patient
                    {
                        Id = 2,
                        NHSNumber = "9434765919",
                        Name = "Aisha Patel",
                        DateOfBirth = new DateTime(1992, 8, 25),
                        GPPractice = "Delapre Medical Centre Northampton"
                    },
                    new Patient
                    {
                        Id = 3,
                        NHSNumber = "4010232137",
                        Name = "Emily Brown",
                        DateOfBirth = new DateTime(1978, 11, 3),
                        GPPractice = "Abington Medical Centre Northampton"
                    },
                    new Patient
                    {
                        Id = 4,
                        NHSNumber = "6215478930",
                        Name = "David Wilson",
                        DateOfBirth = new DateTime(2001, 2, 18),
                        GPPractice = "Kingsthorpe Medical Centre Northampton"
                    }
                };
        public async Task<Patient?> GetPatientByIdAsync(int patientId)
        {
            _logger.LogInformation("Looking up patient in the In-Memory list. PatientId: {PatientId}", patientId);
            var patient = Patients.FirstOrDefault(p => p.Id == patientId);
            return await Task.FromResult(patient);
        }
    }
}
