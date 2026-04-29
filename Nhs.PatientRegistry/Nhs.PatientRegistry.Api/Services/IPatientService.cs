using Nhs.PatientRegistry.Api.DTOs;

namespace Nhs.PatientRegistry.Api.Services
{
    
    public interface IPatientService
    {

        /// <summary>
        /// Retrieves a patient details by id
        /// </summary>
        /// <param name="id">The patient's unique ID.</param>
        /// <returns>PatientDetailsDto</returns>
        Task<PatientDetailsDto?> GetPatientDetailsByIdAsync(int patientId);
    }
}
