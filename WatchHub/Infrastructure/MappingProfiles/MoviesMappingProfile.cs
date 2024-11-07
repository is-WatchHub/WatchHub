using AutoMapper;
using MoviesApplication.Dtos.Incoming;
using MoviesApplication.Dtos.Outgoing;
using MoviesDomain;

namespace Infrastructure.MappingProfiles;

public class MoviesMappingProfile : Profile
{
    public MoviesMappingProfile()
    {
        CreateMap<CreateMovieDto, Movie>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest=>dest.Title,opt=>opt.MapFrom(src=>src.Title))
            .ForMember(dest=>dest,opt=>opt.MapFrom(src=>src.Description))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src=>ConvertToGenre(src.Genre)))
            .ForMember(dest => dest.Media, opt => opt.MapFrom(src => new MovieMedia
            {
                ContentUrl = src.ContentUrl,
                CoverImageUrl = src.CoverImageUrl
            }))
            .ReverseMap()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.ToString()))
            .ForMember(dest => dest.ContentUrl, opt => opt.MapFrom(src => src.Media.ContentUrl))
            .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom(src => src.Media.CoverImageUrl));
        
        CreateMap<Movie, AdditionalMovieInfoResponseDto>()
            .ForMember(dest => dest.ContentUrl, opt => opt.MapFrom(src => src.Media.ContentUrl))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt=>opt.Ignore())
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Genre, opt=>opt.Ignore())
            .ForMember(dest => dest.Media, opt => opt.MapFrom(src => new MovieMedia
            {
                ContentUrl = src.ContentUrl,
                CoverImageUrl = string.Empty
            }));
        
        CreateMap<Movie, FullResponseMovieDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
            .ForMember(dest => dest.ContentUrl, opt => opt.MapFrom(src => src.Media.ContentUrl))
            .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom(src => src.Media.CoverImageUrl))
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ConvertToGenre(src.Genre)))
            .ForMember(dest => dest.Media, opt => opt.MapFrom(src => new MovieMedia
            {
                ContentUrl = src.ContentUrl,
                CoverImageUrl = src.CoverImageUrl
            }));
        
        CreateMap<Movie, MovieInfoResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
            .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom(src => src.Media.CoverImageUrl))
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt=>opt.Ignore())
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
            .ForMember(dest => dest.Media, opt => opt.MapFrom(src => new MovieMedia
            {
                ContentUrl = string.Empty,
                CoverImageUrl = src.CoverImageUrl
            }));
    }
    
    private static Genre ConvertToGenre(string genre)
    {
        return Enum.TryParse<Genre>(genre, true, out var parsedGenre) ? parsedGenre : Genre.Comedy;
    }
}