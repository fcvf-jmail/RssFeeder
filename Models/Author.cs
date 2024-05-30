namespace RssFeeder;
public class Author(string name)
{
    private string Name { get; set; } = name;
    public object ToJson()
    {
        return new { name = Name };
    }
}