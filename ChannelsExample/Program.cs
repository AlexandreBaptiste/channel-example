using ChannelsExample;
using System.Threading.Channels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<ChannelProcessor>();

builder.Services.AddSingleton(
    _ => Channel.CreateBounded<DummyChannelRequest>(new BoundedChannelOptions(1)
    {
        FullMode = BoundedChannelFullMode.Wait,
        SingleReader = true,
        AllowSynchronousContinuations = false
    }
));

var app = builder.Build();

app.MapGet("channel/{name}", async (Channel<DummyChannelRequest> channel, string name) =>
{
    await channel.Writer.WriteAsync(new DummyChannelRequest($"Hello from {name} at {DateTime.UtcNow}"));

    return Results.Ok();
});

app.Run();

public partial class Program { }