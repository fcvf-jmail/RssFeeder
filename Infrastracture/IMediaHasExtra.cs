namespace RssFeeder;
public interface IMediaHasExtra : IMedia
{
    protected string Title { get; set; }
    protected string Extra { get; set; }
    protected string ToString() => $"Type: {Type}\nTitle: {Title}\nExtra: {Extra}";
}