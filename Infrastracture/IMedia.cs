namespace RssFeeder;

public interface IMedia
{
    public MediaType Type { get; set; }
    public abstract string ToHtml();
}