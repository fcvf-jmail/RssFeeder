namespace RssFeeder;

public abstract class MediaHasUrl(string url) : IMedia, IMediaHasUrl
{
    public MediaType Type { get; set; }
    public string Url { get; set; } = url;

    public abstract string ToHtml();

    public override string ToString() => $"Type: {Type}\nUrl: {Url}";
}