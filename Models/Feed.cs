namespace RssFeeder;
using Microsoft.Extensions.Configuration;
public class Feed
{
    private string Version { get; set; }
    private string Title { get; set; }
    private string HomePageUrl { get; set; }
    private string FeedUrl { get; set; }
    private string Icon { get; set; } = "https://t.me/favicon.ico";
    private string FavIcon { get; set; } = "https://t.me/favicon.ico";
    private List<object> JsonItems { get; set; }
    private List<Post> Postes { get; set; }

    public Feed(IConfiguration configuration, string channelTitle, string channelUsername, List<Post> postes)
    {
        Version = configuration["FeedConfig:Version"] ?? "";
        Icon = configuration["FeedConfig:Icon"] ?? "";
        FavIcon = configuration["FeedConfig:FavIcon"] ?? "";
        Title = $"{channelTitle} (@{channelUsername}) - Telegram";
        HomePageUrl = $"https://t.me/s/{channelUsername}";
        FeedUrl = $"{configuration["FeedConfig:BaseUrl"]}/{channelUsername}";
        Postes = postes;
        JsonItems = GetItems();
    }
    private List<object> GetItems()
    {
        List<object> items = [];
        foreach (Post post in Postes)
        {
            Item item = post.ToItem();
            items.Add(item.ToJson());
        }
        return items;
    }

    public object ToJson()
    {
        return new
        {
            version = Version ,
            title = Title ,
            home_page_url = HomePageUrl,
            feed_url = FeedUrl ,
            icon = Icon ,
            favicon = FavIcon,
            items = JsonItems 
        };
    }
}