using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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
                return BadRequest(new
                {
                    Message = "Invalid patient ID.",
                    Errors = validationResult.Errors.Select(error => error.ErrorMessage)
                });
            }

            var patientSummary = await _patientService.GetPatientDetailsByIdAsync(id);

            if (patientSummary is null)
            {
                _logger.LogInformation("Patient with ID {PatientId} was not found.", id);

                return NotFound(new
                {
                    Message = $"Patient with ID {id} was not found."
                });
            }

            return Ok(patientSummary);
        }
    }
}
