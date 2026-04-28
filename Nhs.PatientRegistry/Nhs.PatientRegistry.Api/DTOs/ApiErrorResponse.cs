namespace Nhs.PatientRegistry.Api.DTOs
{
    /// <summary>
    /// The standard format for all error messages.
    /// This ensures that every error looks the same to the user.
    /// </summary>
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Detail { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
