using Nhs.PatientRegistry.Api.DTOs;

namespace Nhs.PatientRegistry.Api.Services
{
    public interface IPatientService
    {
        Task<PatientDetailsDto?> GetPatientDetailsByIdAsync(int patientId);
    }
}
