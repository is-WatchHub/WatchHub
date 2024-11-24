using AutoMapper;
using IntegrationApplication.Mappers;

namespace Infrastructure.Mappers;

public class IntegrationMapper : IIntegrationMapper
{
    private readonly IMapper _mapper;

    public IntegrationMapper(IMapper mapper) => _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public TDestination Map<TSource, TDestination>(TSource source) => _mapper.Map<TSource, TDestination>(source);
}