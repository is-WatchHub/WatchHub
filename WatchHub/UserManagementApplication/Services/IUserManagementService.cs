using UserManagementApplication.Dtos;

namespace UserManagementApplication.Services;

public interface IUserManagementService
{
    public Task<UserDto> GetByUserNameAsync(string username);
}