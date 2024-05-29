using RssFeeder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

var configuration = app.Services.GetRequiredService<IConfiguration>();

builder.WebHost.UseUrls("http://0.0.0.0:2817");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("rssFeeder/{*channelUsername}", (string channelUsername) =>
{
    var channel = new Channel(channelUsername);
    Feed feed = channel.ToFeed(configuration);
    return feed.ToJson();
});

app.Run();