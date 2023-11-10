using MediPortalAuthentication.Model;

namespace MediPortalAuthentication.Services.IServices
{
    public interface IJwtService
    {
        string GenerateToken(User user, IEnumerable<string> roles);
    }
}
