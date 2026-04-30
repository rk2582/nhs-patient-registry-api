namespace Nhs.PatientRegistry.Api.DTOs
{
    public class PatientDetailsDto
    {
        public int Id { get; set; }
        public string NHSNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string GPPractice { get; set; } = string.Empty;
        
    }
}
