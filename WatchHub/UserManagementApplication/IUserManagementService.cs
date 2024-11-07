
using UserManagementApplication.Dtos;

namespace UserManagementApplication.Interfaces;

public interface IUserManagementService
{
    public Task Login(LoginDto model);
    public Task Logout();
    public Task CreateUser(CreateUserDto dto);
}