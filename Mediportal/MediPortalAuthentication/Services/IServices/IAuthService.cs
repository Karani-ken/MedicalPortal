using MediPortalAuthentication.Model.Dtos;
using MediPortalAuthentication.Model;

namespace MediPortalAuthentication.Services.IServices
{
    public interface IAuthService
    {
        Task<string> RegisterUser(RegisterRequestDto newUser);
        Task<string> DeleteUser(Guid userId);
        Task<List<User>> GetUsers();
        Task<string> UpdateUser(User updatedUser);


        Task<LoginResponseDto> LoginUser(LoginRequestDto Loginrequest);

        Task<bool> AssignUserRole(string UserId, string Rolename);
    }
}
