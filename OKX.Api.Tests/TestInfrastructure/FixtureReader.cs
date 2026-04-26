namespace OKX.Api.Tests.TestInfrastructure;

internal static class FixtureReader
{
    public static string Read(params string[] segments)
    {
        var path = TestPaths.Fixture(segments);
        return File.ReadAllText(path);
    }

    public static string ReadManual(params string[] segments)
    {
        var path = TestPaths.ManualFixture(segments);
        return File.ReadAllText(path);
    }

    public static string ReadLive(params string[] segments)
    {
        var path = TestPaths.LiveFixture(segments);
        return File.ReadAllText(path);
    }
}
