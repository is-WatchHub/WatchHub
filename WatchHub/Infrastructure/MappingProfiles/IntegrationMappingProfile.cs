using AutoMapper;
using Infrastructure.Models;
using IntegrationDomain;

namespace Infrastructure.MappingProfiles;

public class IntegrationMappingProfile : Profile
{
    public IntegrationMappingProfile()
    {
        CreateMap<IntegrationModel, Integration>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MovieId))
            .ForMember(dest => dest.Associations, opt => opt.MapFrom(src => src.Association));
        
        CreateMap<MoviePlatformAssociationModel, MoviePlatformAssociation>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ExternalPlatformId, opt => opt.MapFrom(src => src.ExternalPlatformId))
            .ForMember(dest => dest.Platform, opt => opt.MapFrom(src => src.Platform));
        
        CreateMap<PlatformModel, Platform>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));

    }
}