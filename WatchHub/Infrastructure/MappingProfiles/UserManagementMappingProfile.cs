using AutoMapper;
using Infrastructure.Dtos;
using UserManagementDomain;

namespace Infrastructure.MappingProfiles;

public class UserManagementMappingProfile : Profile
{
    public UserManagementMappingProfile()
    {
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(_ => Role.User));

        CreateMap<LoginDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

        CreateMap<User, LoginDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
    }
}