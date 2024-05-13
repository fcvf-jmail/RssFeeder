namespace RssFeeder;

class Author(string name)
{
    protected string Name { get; set; } = name;
    public Dictionary<string, string> ToJson() => new() { { "name", Name } };
}