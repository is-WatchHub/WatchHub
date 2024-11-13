using AutoMapper;
using UserManagementApplication.Mappers;

namespace Infrastructure.Mappers;

public class UserManagementMapper : IUserManagementMapper
{
    private readonly IMapper _mapper;

    public UserManagementMapper(IMapper mapper) => _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public TDestination Map<TSource, TDestination>(TSource source) => _mapper.Map<TSource, TDestination>(source);
}