using AutoMapper;
using MoviesApplication.Mappers;

namespace Infrastructure.Mappers;

public class MoviesMapper : IMoviesMapper
{
    private readonly IMapper _mapper;

    public MoviesMapper(IMapper mapper) => _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public TDestination Map<TSource, TDestination>(TSource source) => _mapper.Map<TSource, TDestination>(source);
}