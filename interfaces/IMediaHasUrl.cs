namespace RssFeeder;

public interface IMediaHasUrl : IMedia
{
    public string Url { get; set; }
    public abstract string ToString();
}