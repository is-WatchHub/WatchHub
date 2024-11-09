using UserManagementApplication.Dtos.Incoming;
using UserManagementApplication.Dtos.Outgoing;

namespace UserManagementApplication.Services;

public interface IAuthenticationService
{
    Task<SignInResultDto> Login(LoginDto model);
    Task Logout();
}