namespace OKX.Api.Tests.TestInfrastructure;

internal static class DotEnvLoader
{
    private static readonly Lock SyncRoot = new();
    private static bool _loaded;

    public static void Load()
    {
        lock (SyncRoot)
        {
            if (_loaded)
                return;

            _loaded = true;
            var path = Path.Combine(TestPaths.RepositoryRoot, ".env");
            if (!File.Exists(path))
                return;

            foreach (var line in File.ReadAllLines(path))
            {
                var trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith('#'))
                    continue;

                var separatorIndex = trimmed.IndexOf('=');
                if (separatorIndex <= 0)
                    continue;

                var key = trimmed[..separatorIndex].Trim();
                var value = trimmed[(separatorIndex + 1)..].Trim();

                if ((value.StartsWith('"') && value.EndsWith('"')) || (value.StartsWith('\'') && value.EndsWith('\'')))
                    value = value[1..^1];

                if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(key)))
                    Environment.SetEnvironmentVariable(key, value);
            }
        }
    }
}
