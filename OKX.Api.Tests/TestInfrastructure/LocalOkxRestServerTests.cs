using System.Collections.Concurrent;

namespace OKX.Api.Tests.TestInfrastructure;

public class LocalOkxRestServerTests
{
    [Fact]
    public void ConcurrentFixtureCreation_RegistersDistinctLivePrefixes()
    {
        var servers = new ConcurrentBag<LocalOkxRestServer>();
        try
        {
            Parallel.For(0, 32, _ => servers.Add(new LocalOkxRestServer(new Dictionary<string, string>())));
            Assert.Equal(32, servers.Count);
            Assert.Equal(32, servers.Select(server => server.BaseAddress).Distinct().Count());
        }
        finally
        {
            foreach (var server in servers) server.Dispose();
        }
    }
}
