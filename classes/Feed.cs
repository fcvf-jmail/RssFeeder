namespace RssFeeder;

class Feed
{
    protected string Version { get; set; } = "https://jsonfeed.org/version/1";
    protected string Title { get; set; }
    protected string HomePageUrl { get; set; }
    protected string FeedUrl { get; set; }
    protected string Icon { get; set; } = "https://t.me/favicon.ico";
    protected string FavIcon { get; set; } = "https://t.me/favicon.ico";
    protected List<Post> Postes { get; set; }
    protected List<Dictionary<string, object>> JsonItems { get; set; }

    public Feed(string channelTitle, string channelUsername, List<Post> postes)
    {
        Title = $"{channelTitle} (@{channelUsername}) - Telegram";
        HomePageUrl = $"https://t.me/s/{channelUsername}";
        FeedUrl = $"localhost:8000/{channelUsername}";
        Postes = postes;
        JsonItems = GetItems();
    }
    public List<Dictionary<string, object>> GetItems()
    {
        List<Dictionary<string, object>> items = [];
        foreach (Post post in Postes)
        {
            Item item = post.ToItem();
            items.Add(item.ToJson());
        }
        return items;
    }

    public Dictionary<string, object> ToJson()
    {
        return new Dictionary<string, object>()
        {
            { "version", Version },
            { "title", Title },
            { "home_page_url", HomePageUrl },
            { "feed_url", FeedUrl },
            { "icon", Icon },
            { "favicon", FavIcon },
            { "items", JsonItems }
        };
    }
}