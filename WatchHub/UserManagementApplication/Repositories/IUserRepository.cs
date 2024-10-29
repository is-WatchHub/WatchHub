using UserManagementDomain;

public interface IUserRepository
{
    public User CreateUser(User user);
    public User GetUserByID(Guid id);
    public User? FindUserByID(Guid id);
}