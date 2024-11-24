namespace IntegrationApplication.Mappers;

public interface IIntegrationMapper
{
    TDestination Map<TSource, TDestination>(TSource source);
}