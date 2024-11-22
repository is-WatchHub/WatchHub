using AutoMapper;
using MoviesApplication.Dtos.Incoming;
using MoviesApplication.Dtos.Outcoming;
using MoviesDomain;

namespace Infrastructure.MappingProfiles;

public class MoviesMappingProfile : Profile
{
    public MoviesMappingProfile()
    {
        CreateMap<CreateMovieDto, MovieMedia>()
            .ForMember(dest => dest.ContentUrl, opt => opt.MapFrom(src => src.ContentUrl))
            .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom(src => src.CoverImageUrl));

        CreateMap<CreateMovieDto, Movie>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest=>dest.Title,opt=>opt.MapFrom(src=>src.Title))
            .ForMember(dest=>dest.Description,opt=>opt.MapFrom(src=>src.Description))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src=>ConvertToGenre(src.Genre)))
            .ForMember(dest => dest.Media, opt => opt.MapFrom(src => src))
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
            .ForMember(dest => dest.Media, opt => opt.MapFrom(src => src));
        
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
            .ForMember(dest => dest.Media, opt => opt.MapFrom(src => src));
        
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
            .ForMember(dest => dest.Media, opt => opt.MapFrom(src => src));
        
        CreateMap<MovieMedia, CreateMovieDto>()
            .ForMember(dest => dest.ContentUrl, opt => opt.MapFrom(src => src.ContentUrl))
            .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom(src => src.CoverImageUrl));
    }
    
    private static Genre ConvertToGenre(string genre)
    {
        if (Enum.TryParse<Genre>(genre, true, out var parsedGenre))
        {
            return parsedGenre;
        }

        throw new ArgumentException($"The genre '{genre}' is not a valid Genre.");
    }
}