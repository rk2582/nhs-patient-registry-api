namespace Nhs.PatientRegistry.Api.Models;


/// <summary>
/// Represents NHS Patient record
/// </summary>
public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string NHSNumber { get; set; } = string.Empty;
    public string GPPractice { get; set; } = string.Empty;
}
