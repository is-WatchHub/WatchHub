using AutoMapper;
using UserManagementApplication.Dtos;
using UserManagementDomain;

namespace Infrastructure.MappingProfiles;

public class UserManagementMappingProfile : Profile
{
    public UserManagementMappingProfile()
    {
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => HashPassword(src.Password)))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(_ => Role.User));
        
        CreateMap<LoginDto, User>()
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => HashPassword(src.Password)));

        CreateMap<User, CreateUserDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.Ignore());
        
        CreateMap<User, LoginDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.Password, opt => opt.Ignore());

    }

    private static string HashPassword(string password) => password;
}