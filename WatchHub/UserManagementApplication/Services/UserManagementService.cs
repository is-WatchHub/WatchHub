using UserManagementApplication.Dtos;
using UserManagementApplication.Mappers;
using UserManagementApplication.Repositories;
using UserManagementDomain;

namespace UserManagementApplication.Services;

public class UserManagementService : IUserManagementService
{
    private readonly IUserManagementMapper _mapper;
    private readonly IUserRepository _repository;

    public UserManagementService(IUserManagementMapper mapper, IUserRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<UserDto> GetByUserNameAsync(string username)
    {
        var user = await _repository.GetByUserNameAsync(username);
        var result = _mapper.Map<User, UserDto>(user);

        return result;
    }
}