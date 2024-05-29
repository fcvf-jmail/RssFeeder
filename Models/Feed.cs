namespace RssFeeder;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
public class Feed
{
    [JsonProperty("version")]
    private string Version { get; set; }

    [JsonProperty("title")]
    private string Title { get; set; }

    [JsonProperty("home_page_url")]
    private string HomePageUrl { get; set; }

    [JsonProperty("feed_url")]
    private string FeedUrl { get; set; }

    [JsonProperty("icon")]
    private string Icon { get; set; } = "https://t.me/favicon.ico";

    [JsonProperty("favicon")]
    private string FavIcon { get; set; } = "https://t.me/favicon.ico";

    [JsonProperty("items")]
    private List<string> JsonItems { get; set; }
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
    private List<string> GetItems()
    {
        List<string> items = [];
        foreach (Post post in Postes)
        {
            Item item = post.ToItem();
            items.Add(item.ToJson());
        }
        return items;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}