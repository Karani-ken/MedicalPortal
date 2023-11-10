using System.ComponentModel.DataAnnotations;

namespace MediPortalAuthentication.Model.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
