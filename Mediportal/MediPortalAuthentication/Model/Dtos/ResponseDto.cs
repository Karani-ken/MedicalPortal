namespace MediPortalAuthentication.Model.Dtos
{
    public class ResponseDto
    {
        public object? obj { get; set; }

        public bool IsSuccess { get; set; } = true;

        public string Message { get; set; } = string.Empty;
    }
}
