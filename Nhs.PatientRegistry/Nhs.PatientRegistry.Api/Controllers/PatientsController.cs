using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Nhs.PatientRegistry.Api.DTOs;
using Nhs.PatientRegistry.Api.Services;

namespace Nhs.PatientRegistry.Api.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IValidator<int> _patientIdValidator;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(IPatientService patientService, IValidator<int> patientIdValidator, ILogger<PatientsController> logger)
        {
            _patientService = patientService;
            _patientIdValidator = patientIdValidator;
            _logger = logger;
        }


        /// <summary>
        /// Gets the patient details using the patient ID.
        /// </summary>
        /// <param name="id">The patient ID.</param>
        /// <returns>The patient details if found.</returns>

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PatientDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPatientDetailsById(int id)
        {
            var validationResult = await _patientIdValidator.ValidateAsync(id);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Invalid patient ID supplied: {PatientId}", id);

                return BadRequest(new ApiErrorResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Patient ID must be a positive integer.",
                    Detail = validationResult.Errors.FirstOrDefault()?.ErrorMessage
                });
            }

            var patientDetails = await _patientService.GetPatientDetailsByIdAsync(id);



            if (patientDetails is null)
            {
                _logger.LogInformation("Patient with ID {PatientId} was not found.", id);

                return NotFound(new ApiErrorResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Patient with ID {id} was not found.",
                    Detail = $"No patient record was found for ID {id}. Please check the ID and try again."
                });
            }

            return Ok(patientDetails);
        }

    }
}
