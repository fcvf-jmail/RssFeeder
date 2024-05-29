namespace RssFeeder;
using Newtonsoft.Json;

public class Item
{
    [JsonProperty("id")]
    protected string Id { get; set; }
    [JsonProperty("title")]
    protected string Title { get; set; }
    [JsonProperty("author")]
    protected Author Author { get; set; }
    [JsonProperty("date_modified")]
    protected string DateModified { get; set; }
    [JsonProperty("url")]
    protected string Url { get; set; }
    [JsonProperty("content_html")]
    protected string ContentHtml { get; set; }

    public Item(string id, string title, string authorName, string dateModified, string url, string contentHtml)
    {
        (Id, Title, Author, DateModified, Url, ContentHtml) = (id, title, new Author(authorName), dateModified, url, contentHtml);
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}