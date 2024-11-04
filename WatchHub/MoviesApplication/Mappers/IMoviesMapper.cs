namespace MoviesApplication.Mappers;

public interface IMoviesMapper
{
    TDestination Map<TSource, TDestination>(TSource source);
}