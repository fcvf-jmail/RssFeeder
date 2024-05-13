namespace RssFeeder;
public interface IMediaHasExtra : IMedia
{
    public string Title { get; set; }
    public string Extra { get; set; }
    public string ToString() => $"Type: {Type}\nTitle: {Title}\nExtra: {Extra}";
}