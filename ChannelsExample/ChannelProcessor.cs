using System.Threading.Channels;

namespace ChannelsExample;

public class ChannelProcessor(Channel<DummyChannelRequest> channel) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(await channel.Reader.WaitToReadAsync(stoppingToken))
        {
            var request = await channel.Reader.ReadAsync(stoppingToken);  
            await Task.Delay(2000, stoppingToken);
            Console.WriteLine($"Received request: {request.Message}");
        }
    }
}
