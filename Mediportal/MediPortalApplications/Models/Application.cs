namespace MediPortalApplications.Models
{
    public class Application
    {
        public Guid ApplicationId { get; set; }
        public string firstname { get; set; } = string.Empty;
        public string lastname { get; set; } = string.Empty;
        public string surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string speciality { get; set; } = string.Empty;
        public string LicenseUrl { get; set; } = string.Empty;
        public string HospitalName { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
