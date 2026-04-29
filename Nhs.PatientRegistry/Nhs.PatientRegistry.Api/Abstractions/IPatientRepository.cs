using Nhs.PatientRegistry.Api.Models;

namespace Nhs.PatientRegistry.Api.Abstractions
{
    public interface IPatientRepository
    {
        /// <summary>
        /// Returns a patient by their unique patient ID.
        /// </summary>
        /// <param name="patientId">The patient's unique ID.</param>
        /// <returns>The matching patient if found; otherwise null.</returns>
        Task<Patient?> GetPatientByIdAsync(int patientId);
    }
}
