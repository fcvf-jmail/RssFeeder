using System.IO.Pipelines;
using HtmlAgilityPack;

namespace RssFeeder;
class Channel
{
    HtmlNode HtmlNode { get; set; }
    string Title { get; set; }
    string Username { get; set; }
    string Url { get; set; }

    List<Post> Postes { get; set; }

    public Channel(string channelUsername)
    {
        Username = channelUsername;
        Url = $"https://t.me/s/{Username}";
        HtmlNode = GetHtmlAsync().Result;
        Title = GetTitle();
        Postes = GetPostes();
    }

    protected async Task<HtmlNode> GetHtmlAsync()
    {
        string responseData = await MakeGetRequest(Url);
        HtmlDocument doc = new();
        doc.LoadHtml(responseData);
        return doc.DocumentNode;
    }

    protected string GetTitle()
    {
        HtmlNode channelNameElement = HtmlNode.SelectSingleNode(".//div[contains(concat(' ',normalize-space(@class),' '),' tgme_channel_info_header_title ')]/span");
        return channelNameElement?.InnerText ?? "";
    }
    public List<Post> GetPostes()
    {
        List<Post> Postes = [];
        HtmlNodeCollection PostesNodes = HtmlNode.SelectNodes(".//div[contains(concat(' ',normalize-space(@class),' '),' tgme_widget_message_bubble ')]");
        foreach (HtmlNode PostNode in PostesNodes)
        {
            Post post = new(PostNode, Title);
            if (post.IsSupported()) Postes.Add(post);
        }
        return Postes;
    }

    public Feed ToFeed()
    {
        return new Feed(Title, Username, Postes);
    }

    static async Task<string> MakeGetRequest(string url)
    {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
        else return "Error: " + response.StatusCode;
    }
}