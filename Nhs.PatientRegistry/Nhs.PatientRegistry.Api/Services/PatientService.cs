using AutoMapper;
using Nhs.PatientRegistry.Api.Abstractions;
using Nhs.PatientRegistry.Api.DTOs;
namespace Nhs.PatientRegistry.Api.Services
{

    /// <summary>
    /// The main logic for handling patient information.
    /// </summary>
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientService> _logger;

        public PatientService(ILogger<PatientService> logger, IPatientRepository patientRepository, IMapper mapper)
        {
            _logger = logger;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Looks up a patient by their ID.
        /// If found, returns patient data. If not found, we log a warning and return null.
        /// </summary>
        /// <param name="patientId">The patient's unique ID.</param>
        public async Task<PatientDetailsDto?> GetPatientDetailsByIdAsync(int patientId)
        {
            _logger.LogInformation("Getting patient details for PatientId: {PatientId}", patientId);
            var patients = await _patientRepository.GetPatientsAsync();

            var patient = patients.FirstOrDefault(p => p.Id == patientId);
            if (patient is null)
            {
                _logger.LogWarning("Patient details not found for PatientId: {PatientId}", patientId);
                return null;
            }
            _logger.LogInformation("Patient details found for PatientId: {PatientId}", patientId);
            return _mapper.Map<PatientDetailsDto>(patient);

        }


    }
}
