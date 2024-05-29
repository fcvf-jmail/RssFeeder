namespace RssFeeder;

public interface IMediaHasUrl : IMedia
{
    protected string Url { get; set; }
    protected abstract string ToString();
}