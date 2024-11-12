using IntegrationApplication.Dtos;
using IntegrationDomain;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Handlers;

public class OmdbHandler : RequestHandler
{
    private readonly string _apiKey;
    
    public OmdbHandler(string apiKey, string name) : base(name) => 
        _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));

    protected override string PreparingRequest(MoviePlatformAssociation association, HttpClient client) =>
        association.Platform.Url + $"/?apikey={_apiKey}&i={association.ExternalPlatformId}";

    protected override void Collect(JObject jsonResponse, MovieInformationDto movieInformationDto) => 
        CollectAwards(jsonResponse, movieInformationDto);

    private void CollectAwards(JObject jsonResponse, MovieInformationDto movieInformationDto) => 
        movieInformationDto.Awards = jsonResponse["Awards"]?.ToObject<string>() ?? "";
}