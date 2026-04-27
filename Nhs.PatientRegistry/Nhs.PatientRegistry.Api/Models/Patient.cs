namespace Nhs.PatientRegistry.Api.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string NHSNumber { get; set; } = string.Empty;
        public string GPPractice { get; set; } = string.Empty;
    }
}
