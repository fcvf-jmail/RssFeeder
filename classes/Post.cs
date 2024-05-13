namespace RssFeeder;

using System;
using System.Text;
using HtmlAgilityPack;

class Post
{
    string Url { get; set; }
    HtmlNode HtmlNode { get; set; }
    string Author { get; set; }
    string Text { get; set; }
    string FormattedText { get; set; }
    string Title { get; set; }
    string DateModified { get; set; }
    protected List<IMedia> Media { get; set; }
    protected string ContentHtml { get; set; }

    public Post (HtmlNode htmlNode, string author)
    {
        HtmlNode = htmlNode;
        Url = GetUrl();
        Author = author;
        Text = GetText();
        FormattedText = GetFormattedText();
        Title = GetTitle();
        DateModified = GetDateModifed();
        Media = GetMedia();
        ContentHtml = GetContentHtml();
    }

    protected string GetUrl()
    {
        HtmlNode urlElement = HtmlNode.SelectSingleNode(".//a[contains(concat(' ',normalize-space(@class),' '),' tgme_widget_message_date ')]");
        return urlElement.GetAttributeValue("href", "https://example.com");
    }

    protected string GetFormattedText()
    {
        HtmlNode textElement = HtmlNode.SelectSingleNode(".//div[contains(concat(' ',normalize-space(@class),' '),' tgme_widget_message_text ')]");
        return textElement?.InnerHtml ?? "";
    }
    protected string GetText()
    {
        HtmlNode textElement = HtmlNode.SelectSingleNode(".//div[contains(concat(' ',normalize-space(@class),' '),' tgme_widget_message_text ')]");
        return textElement?.InnerText ?? "";
    }


    protected string GetTitle()
    {
        string[] words = Text.Split(" ");
        if (words.Length == 0) return $"{Author} published new post";
        StringBuilder title = new();
        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            if (title.Length + word.Length > 55)
            {
                title.Append("...");
                break;
            }
            title.Append((i == 0 ? "" : " ") + word);
        }
        return title.ToString();
    }

    protected string GetDateModifed()
    {
        HtmlNode dateElement = HtmlNode.SelectSingleNode(".//time[contains(concat(' ',normalize-space(@class),' '),' time ')]");
        return dateElement.GetAttributeValue("datetime", new DateTime().ToString());
    }

    protected List<IMedia> GetMedia()
    {
        List<IMedia> medias = [];
        List<Video> videos = GetVideos();
        List<Photo> photos = GetPhotos();
        List<Document> documents = GetDocuments();
        List<VoiceMessage> voiceMessages = GetVoiceMessages();

        medias.AddRange(videos);
        medias.AddRange(photos);
        medias.AddRange(documents);
        medias.AddRange(voiceMessages);

        return medias;
    }

    protected List<Video> GetVideos()
    {
        List<Video> videos = [];
        HtmlNodeCollection videoElements = HtmlNode.SelectNodes(".//video[contains(concat(' ',normalize-space(@class),' '),' js-message_video ')]");
        if (videoElements is null) return videos;
        foreach (HtmlNode videoElement in videoElements)
        {
            string videoUrl = videoElement.GetAttributeValue("src", "https://example.com");
            Video video = new(videoUrl);
            videos.Add(video);
        }
        return videos;
    }
    protected List<Photo> GetPhotos()
    {
        List<Photo> photos = [];
        HtmlNodeCollection photoElements = HtmlNode.SelectNodes(".//a[contains(concat(' ',normalize-space(@class),' '),' tgme_widget_message_photo_wrap ')]");
        if (photoElements is null) return photos;
        foreach (HtmlNode photoElement in photoElements)
        {
            string styleAttributeValue = photoElement.GetAttributeValue("style", "").Replace("&quot;", "");

            string urlStartMarker = "background-image:url('";
            string urlEndMarker = "')";

            int startIndex = styleAttributeValue.IndexOf(urlStartMarker) + urlStartMarker.Length;
            int endIndex = styleAttributeValue.IndexOf(urlEndMarker, startIndex);

            string url = styleAttributeValue[startIndex..endIndex];

            Photo photo = new(url);
            photos.Add(photo);
        }
        return photos;
    }

    protected List<Document> GetDocuments()
    {
        List<Document> documents = [];
        HtmlNodeCollection documentElemnts = HtmlNode.SelectNodes(".//div[contains(concat(' ',normalize-space(@class),' '),' tgme_widget_message_document ')]");
        if (documentElemnts is null) return documents;
        foreach (HtmlNode documentElement in documentElemnts)
        {
            HtmlNode documentTitleElement = documentElement.SelectSingleNode(".//div[contains(concat(' ',normalize-space(@class),' '),' tgme_widget_message_document_title ')]");
            HtmlNode documentSizeElement = documentElement.SelectSingleNode(".//div[contains(concat(' ',normalize-space(@class),' '),' tgme_widget_message_document_extra ')]");
            Document document = new(documentTitleElement.InnerText, documentSizeElement.InnerText);
            documents.Add(document);
        }
        return documents;
    }

    protected List<VoiceMessage> GetVoiceMessages()
    {
        List<VoiceMessage> voiceMessages = [];
        HtmlNodeCollection voiceMessageElements = HtmlNode.SelectNodes(".//audio[contains(concat(' ',normalize-space(@class),' '),' tgme_widget_message_voice ')]");
        if (voiceMessageElements is null) return voiceMessages;
        foreach (HtmlNode voiceMessageElement in voiceMessageElements)
        {
            string voiceMessageUrl = voiceMessageElement.GetAttributeValue("src", "https://example.com");
            VoiceMessage voiceMessage = new(voiceMessageUrl);
            voiceMessages.Add(voiceMessage);
        }
        return voiceMessages;
    }
    protected string GetContentHtml()
    {
        string htmlContent = MediaToHtml();
        htmlContent += $"{FormattedText}";
        return htmlContent;
    }

    protected string MediaToHtml()
    {
        StringBuilder htmlContent = new();
        foreach (IMedia media in Media) htmlContent.Append(media.ToHtml());
        if (htmlContent.Length > 0) htmlContent.Append("<br><br>");
        return htmlContent.ToString();
    }

    public bool IsSupported()
    {
        bool x = ContentHtml.Length != 0;
        return x;
    }

    public Item ToItem()
    {
        return new Item(Url, Title, Author, DateModified, Url, ContentHtml);
    }
}