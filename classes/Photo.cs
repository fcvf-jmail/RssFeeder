namespace RssFeeder;

public class Photo : MediaHasUrl
{
    public Photo(string url) : base(url)
    {
        Type = MediaType.Photo;
    }

    public override string ToHtml() => $"<a href=\"{Url}\"><img src=\"{Url}\"></a>";
}