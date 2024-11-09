using UserManagementApplication.Dtos;
using UserManagementApplication.Dtos.Results;

namespace UserManagementApplication;

public interface IUserManagementService
{
    public Task<SignInResultDto> Login(LoginDto model);
    public Task Logout();
    public Task<CreateUserResultDto> CreateUser(CreateUserDto dto);
}