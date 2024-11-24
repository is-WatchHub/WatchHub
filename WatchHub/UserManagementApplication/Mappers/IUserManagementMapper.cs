namespace UserManagementApplication.Mappers;

public interface IUserManagementMapper
{
    TDestination Map<TSource, TDestination>(TSource source);
}