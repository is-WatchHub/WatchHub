using IntegrationApplication.Dtos;
using IntegrationApplication.Handlers;
using IntegrationDomain;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Handlers;

public abstract class RequestHandler : IRequestHandler
{
    private IRequestHandler? _nextHandler;
    private readonly HttpClient _client;
    
    private readonly string _name;
    
    public RequestHandler(string name)
    {
        _name = name ?? throw new ArgumentNullException(nameof(name));
        _client = new HttpClient();
    }
    
    public IRequestHandler SetNext(IRequestHandler handler)
    {
        _nextHandler = handler ?? throw new ArgumentNullException(nameof(handler));
        return handler;
    }

    public async Task CollectMovieInformation(Integration integration, MovieInformationDto movieInformationDto)
    {
        var association = integration.Platforms
            .FirstOrDefault(p => p.Platform.Name == _name);

        if (association is null)
        {
            _nextHandler?.CollectMovieInformation(integration, movieInformationDto);
            
            return;
        }

        var requestUri = PreparingRequest(association, _client);
        
        var response = await _client.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            _nextHandler?.CollectMovieInformation(integration, movieInformationDto);
            
            return;
        }
        
        var responseBody = await response.Content.ReadAsStringAsync();
        
        var jsonResponse = JObject.Parse(responseBody);
        
        Collect(jsonResponse, movieInformationDto);

        _nextHandler?.CollectMovieInformation(integration, movieInformationDto);
    }

    protected virtual string PreparingRequest(MoviePlatformAssociation association, HttpClient client) =>
        association.Platform.Url;
    
    protected virtual void Collect(JObject jsonResponse, MovieInformationDto movieInformationDto) { }
}