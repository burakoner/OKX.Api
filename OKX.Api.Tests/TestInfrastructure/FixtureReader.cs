namespace OKX.Api.Tests.TestInfrastructure;

internal static class FixtureReader
{
    public static string Read(params string[] segments)
    {
        var path = TestPaths.Fixture(segments);
        return File.ReadAllText(path);
    }
}
