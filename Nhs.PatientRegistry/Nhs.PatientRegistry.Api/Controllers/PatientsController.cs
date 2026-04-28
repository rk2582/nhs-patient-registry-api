using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Nhs.PatientRegistry.Api.DTOs;
using Nhs.PatientRegistry.Api.Services;

namespace Nhs.PatientRegistry.Api.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientsController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IValidator<int> _patientIdValidator;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(
            IPatientService patientService,
            IValidator<int> patientIdValidator,
            ILogger<PatientsController> logger)
        {
            _patientService = patientService;
            _patientIdValidator = patientIdValidator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientDetailsById(int id)
        {
            var validationResult = await _patientIdValidator.ValidateAsync(id);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning($"Invalid patient ID supplied: {id}");

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
