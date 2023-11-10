using AutoMapper;
using MediPortalAuthentication.Data;
using MediPortalAuthentication.Model;
using MediPortalAuthentication.Model.Dtos;
using MediPortalAuthentication.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MediPortalAuthentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtService _jwtGenerator;
      
        public AuthService(IJwtService jwtToken, ApplicationDbContext context, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtGenerator = jwtToken;
          
        }
        public async Task<bool> AssignUserRole(string UserId, string Rolename)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToLower() == UserId.ToLower());

                if (user != null)
                {
                    if (!_roleManager.RoleExistsAsync(Rolename).GetAwaiter().GetResult())
                    {

                        _roleManager.CreateAsync(new IdentityRole(Rolename)).GetAwaiter().GetResult();
                    }
                    await _userManager.AddToRoleAsync(user, Rolename);
                    user.Status = "Approved";
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                    return true;

                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteUser(Guid userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return "User not found";
                }


                var res = await _userManager.DeleteAsync(user);

                if (res.Succeeded)
                {


                    return "";
                }
                else
                {
                    return "failed to delete user.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<List<User>> GetUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();

                return users;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoginResponseDto> LoginUser(LoginRequestDto Loginrequest)
        {
            try
            {
                //check if user exists
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == Loginrequest.Username.ToLower());

                var passwordIsCorrect = await _userManager.CheckPasswordAsync(user, Loginrequest.password);

                if (!passwordIsCorrect || user == null)
                {
                    new LoginRequestDto();
                }
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtGenerator.GenerateToken(user, roles);
                var LoggedInUser = new LoginResponseDto()
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = token
                };

                return LoggedInUser;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> RegisterUser(RegisterRequestDto newUser)
        {
            var user = _mapper.Map<User>(newUser);
            try
            {
                var res = await _userManager.CreateAsync(user, newUser.Password);               
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async  Task<string> UpdateUser(User updatedUser)
        {
            try
            {
                _context.Users.Update(updatedUser);
                await _context.SaveChangesAsync();
                return "user updated successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
