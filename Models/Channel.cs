using HtmlAgilityPack;

namespace RssFeeder;
public class Channel
{
    private HtmlNode HtmlNode { get; set; }
    private string Title { get; set; }
    private string Username { get; set; }
    private string Url { get; set; }

    private List<Post> Postes { get; set; }

    public Channel(string channelUsername)
    {
        Username = channelUsername;
        Url = $"https://t.me/s/{Username}";
        HtmlNode = GetHtmlAsync().Result;
        Title = GetTitle();
        Postes = GetPostes();
    }

    private async Task<HtmlNode> GetHtmlAsync()
    {
        string responseData = await MakeGetRequest(Url);
        HtmlDocument doc = new();
        doc.LoadHtml(responseData);
        return doc.DocumentNode;
    }

    private string GetTitle()
    {
        HtmlNode channelNameElement = HtmlNode.SelectSingleNode(".//div[contains(concat(' ',normalize-space(@class),' '),' tgme_channel_info_header_title ')]/span");
        return channelNameElement?.InnerText ?? "";
    }
    private List<Post> GetPostes()
    {
        List<Post> Postes = [];
        HtmlNodeCollection PostesNodes = HtmlNode.SelectNodes(".//div[contains(concat(' ',normalize-space(@class),' '),' tgme_widget_message_bubble ')]");
        foreach (HtmlNode PostNode in PostesNodes)
        {
            Post post = new(PostNode, Title);
            Postes.Add(post);
        }
        return Postes;
    }

    public Feed ToFeed(IConfiguration configuration)
    {
        return new Feed(configuration, Title, Username, Postes);
    }

    private static async Task<string> MakeGetRequest(string url)
    {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
        else return "Error: " + response.StatusCode;
    }
}