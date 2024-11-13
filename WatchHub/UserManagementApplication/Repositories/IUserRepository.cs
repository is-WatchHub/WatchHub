using UserManagementDomain;

namespace UserManagementApplication.Repositories;

public interface IUserRepository
{
    Task<User> GetByUserNameAsync(string username);
}