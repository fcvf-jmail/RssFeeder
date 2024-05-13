namespace RssFeeder;

class Document : MediaHasExtra
{
    public Document(string title, string extra) : base(title, extra)
    {
        Type = MediaType.Document;
    }

    public override string ToHtml() => $"File attachment: {Title} - {Extra}<br>";
}