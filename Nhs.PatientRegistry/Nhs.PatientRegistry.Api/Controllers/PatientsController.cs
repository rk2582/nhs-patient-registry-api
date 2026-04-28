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
        public Task<IActionResult> GetPatientDetailsById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
