using IntegrationApplication.Dtos;
using IntegrationDomain;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Handlers;

public class KinopoiskHandler : RequestHandler
{
    private readonly string _apiKey;
    
    public KinopoiskHandler(string apiKey, string name) : base(name) => 
        _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));

    protected override string PreparingRequest(MoviePlatformAssociation association, HttpClient client)
    {
        client.DefaultRequestHeaders.Add("accept", "application/json");
        client.DefaultRequestHeaders.Add("X-API-KEY", _apiKey);
        
        return association.Platform.Url + $"/{association.ExternalPlatformId}";
    }

    protected override void Collect(JObject jsonResponse, MovieInformationDto movieInformationDto)
    {
        CollectPersons(jsonResponse, movieInformationDto);
        
        CollectRating(jsonResponse, movieInformationDto);
    }

    private void CollectPersons(JObject jsonResponse, MovieInformationDto movieInformationDto)
    {
        var persons = jsonResponse["persons"] as JArray;

        if (persons is null) return;

        foreach (var person in persons)
        {
            var profession = person["enProfession"]?.ToObject<string>();
            switch (profession)
            {
                case "actor":
                {
                    var actorName = person["enName"]?.ToObject<string>();
                    if (actorName is not null) movieInformationDto.ActorNames.Add(actorName);
                    break;
                }
                case "director":
                {
                    var director = person["enName"]?.ToObject<string>();
                    if (director is not null) movieInformationDto.Director = director;
                    break;
                }
            }
        }
    }

    private void CollectRating(JObject jsonResponse, MovieInformationDto movieInformationDto)
    {
        const string kp = "kp";
        const string imdb = "imdb";
        const string filmCritics = "filmCritics";

        var ratingKp = jsonResponse["rating"]?[kp]?.ToObject<double>() ?? 0.0;
        var ratingImdb = jsonResponse["rating"]?[imdb]?.ToObject<double>() ?? 0.0;
        var ratingFilmCritics = jsonResponse["rating"]?[filmCritics]?.ToObject<double>() ?? 0.0;

        movieInformationDto.Rating.Ratings
            .Add(new ServiceRatingDto{Service = kp, Rating = ratingKp});
        movieInformationDto.Rating.Ratings
            .Add(new ServiceRatingDto{Service = imdb, Rating = ratingImdb});
        movieInformationDto.Rating.Ratings
            .Add(new ServiceRatingDto{Service = filmCritics, Rating = ratingFilmCritics});
        
        movieInformationDto.Rating.AggregatedValue = 
            movieInformationDto.Rating.Ratings.Average(r => r.Rating);
    }
}