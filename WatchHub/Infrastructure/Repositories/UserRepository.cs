using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagementApplication.Mappers;
using UserManagementApplication.Repositories;
using UserManagementDomain;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserManagementMapper _mapper;

    public UserRepository(UserManager<ApplicationUser> userManager, IUserManagementMapper mapper)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<User> GetByUserNameAsync(string username)
    {
        var user = await _userManager.Users
            .FirstAsync(u => u.UserName == username);

        return _mapper.Map<ApplicationUser, User>(user);
    }
}