namespace RssFeeder;
public class Item
{
    protected string Id { get; set; }
    protected string Title { get; set; }
    protected Author Author { get; set; }
    protected string DateModified { get; set; }
    protected string Url { get; set; }
    protected string ContentHtml { get; set; }

    public Item(string id, string title, string authorName, string dateModified, string url, string contentHtml)
    {
        (Id, Title, Author, DateModified, Url, ContentHtml) = (id, title, new Author(authorName), dateModified, url, contentHtml);
    }

    public object ToJson()
    {
        return new 
        {
            id = Id,
            title = Title,
            author = Author.ToJson(),
            date_modified = DateModified,
            url = Url,
            content_html = ContentHtml,
        };
    }
}