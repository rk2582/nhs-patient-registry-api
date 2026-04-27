using Nhs.PatientRegistry.Api.Models;

namespace Nhs.PatientRegistry.Api.Abstractions
{
    public interface IPatientRepository
    {
        Task<IReadOnlyCollection<Patient>> GetPatientsAsync();
    }
}
