using Nhs.PatientRegistry.Api.Abstractions;
using Nhs.PatientRegistry.Api.DTOs;

namespace Nhs.PatientRegistry.Api.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public Task<PatientDetailsDto?> GetPatientDetailsByIdAsync(int patientId)
        {
            throw new NotImplementedException();
        }
    }
}
