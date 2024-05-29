namespace RssFeeder;

public class Video : MediaHasUrl
{
    public Video(string url) : base(url)
    {
        Type = MediaType.Video;
    }
    public override string ToHtml() => $"<video controls=\"\"><source src=\"{Url}\" type=\"video/mp4\"></video>";
}