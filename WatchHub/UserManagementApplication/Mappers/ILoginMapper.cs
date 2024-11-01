using UserManagementApplication.Dtos;
using UserManagementDomain;

namespace UserManagementApplication.Mappers;

public interface ILoginMapper
{
    User MapToUser(LoginDto loginDto);
    LoginDto MapToLoginDto(User user);
}