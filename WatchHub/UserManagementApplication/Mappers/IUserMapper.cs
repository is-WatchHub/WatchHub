using UserManagementApplication.Dtos;
using UserManagementDomain;

namespace UserManagementApplication.Mappers;

public interface IUserMapper
{
    User MapToUser(CreateUserDto createUserDto);
    CreateUserDto MapToCreateUserDto(User user);
}