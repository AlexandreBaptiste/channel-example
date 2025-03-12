using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace ChannelsExampleTests;

public class ChannelsTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Test1()
    {
        var client = factory.CreateClient();

        List<Task> tasks = [];

        List<string> names = ["Lorem", "Ipsum", "Dolor", "Sit", "Amet", "Elit", "Aenean"];

        foreach (string name in names)
        {
            tasks.Add(Task.Run(async () =>
            {
                var response = await client.GetAsync($"/channel/{name}");
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }));
        }

        Task t = Task.WhenAll(tasks);

        await t.WaitAsync(new CancellationToken());
    }
}
