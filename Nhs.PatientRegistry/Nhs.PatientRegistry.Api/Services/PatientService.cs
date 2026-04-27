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

        public async Task<PatientDetailsDto?> GetPatientDetailsByIdAsync(int patientId)
        {
            var patients = await _patientRepository.GetPatientsAsync();

            var patient = patients.FirstOrDefault(p => p.Id == patientId);

            if (patient is null)
                return null;

            return new PatientDetailsDto
            {
                Id = patient.Id,
                NHSNumber = patient.NHSNumber,
                Name = patient.Name,
                DateOfBirth = patient.DateOfBirth,
                GPPractice = patient.GPPractice,

            };
        }
    }
}
