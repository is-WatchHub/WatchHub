namespace MoviesDomain;

public class VideoPlayer
{
    public Guid Id { get; set; }
    public string ContentUrl { get; set; }
    public VideoPlayerType PlayerType { get; set; }
}