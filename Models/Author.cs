namespace RssFeeder;
using Newtonsoft.Json;

public class Author(string name)
{
    [JsonProperty("name")]
    private string Name { get; set; } = name;
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}