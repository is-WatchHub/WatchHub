using UserManagementApplication.Dtos.Incoming;
using UserManagementApplication.Dtos.Outgoing;

namespace UserManagementApplication.Services;

public interface IUserManagementService
{
    public Task<SignInResultDto> Login(LoginDto model);
    public Task Logout();
    public Task<CreateUserResultDto> CreateUser(CreateUserDto dto);
    public Task<GetUserResultDto?> GetUser(Guid id);
    public Task<GetUserResultDto?> GetUser(string username);
}