using Nhs.PatientRegistry.Api.Models;

namespace Nhs.PatientRegistry.Api.Abstractions
{
    public interface IPatientRepository
    {
/// <summary>
    /// Returns a list of patients that we can loop through.
    /// Note: This is currently using In-Memory data.
    /// </summary>
        Task<IEnumerable<Patient>> GetPatientsAsync();
    }
}
