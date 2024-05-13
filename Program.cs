using RssFeeder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("rssFeeder/{*channelUsername}", (string channelUsername) =>
{
    Channel channel = new(channelUsername);
    Feed feed = channel.ToFeed();
    return feed.ToJson();
});

app.Run();