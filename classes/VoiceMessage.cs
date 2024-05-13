namespace RssFeeder;

public class VoiceMessage : MediaHasUrl
{
    public VoiceMessage(string url) : base(url)
    {
        Type = MediaType.VoiceMessage;
    }
    public override string ToHtml() => $"<audio controls><source src=\"{Url}\">Your browser does not support the audio tag.</audio>";
}