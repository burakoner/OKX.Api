using System.Reflection;

namespace OKX.Api.Tests.TestInfrastructure;

internal static class TestPaths
{
    private static readonly Lazy<string> RepositoryRootPath = new(FindRepositoryRoot);

    public static string RepositoryRoot => RepositoryRootPath.Value;

    public static string Fixture(params string[] segments)
        => Path.Combine([RepositoryRoot, "OKX.Api.Tests", "Fixtures", .. segments]);

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "OKX Api Client.sln")))
                return directory.FullName;

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException($"Could not locate repository root for test assembly {Assembly.GetExecutingAssembly().Location}");
    }
}
