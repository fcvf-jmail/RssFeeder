namespace RssFeeder;

public abstract class MediaHasExtra(string title, string extra) : IMedia, IMediaHasExtra
{
    public MediaType Type { get; set; }
    public string Title { get; set; } = title;
    public string Extra { get; set; } = extra;

    public abstract string ToHtml();

    public override string ToString() => $"Type: {Type}\nTitle: {Title}\nExtra: {Extra}";
}