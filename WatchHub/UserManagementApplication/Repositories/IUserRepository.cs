using UserManagementDomain;

public interface IUserRepository
{
    Task<User> Create(User user);
    Task<User> GetById(Guid id);
    Task<User> GetByUserNameAsync(string login);
    Task<User?> FindById(Guid id);
}