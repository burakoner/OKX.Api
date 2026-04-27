using Xunit;

namespace OKX.Api.Tests.TestInfrastructure;

[CollectionDefinition(Name, DisableParallelization = true)]
public sealed class OkxIntegrationTestCollection
{
    public const string Name = "OKX live integration tests";
}
