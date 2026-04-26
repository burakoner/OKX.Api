using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Account;
using OKX.Api.Base;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Account;

public class OkxAccountBillTypeContractTests
{
    [Fact]
    public void ManualBillTypesFixture_ParsesTypeAndSubtypeMappings()
    {
        var response = Deserialize<OkxAccountBillTypeInfo>("Account", "get-bill-types.json");

        Assert.Equal(2, response.Data!.Count);

        var transfer = response.Data.Single(x => x.TypeId == "1");
        Assert.Equal("Transfer", transfer.TypeDescription);
        Assert.Contains(transfer.SubTypeDetails, x => x.SubTypeId == "11" && x.SubTypeDescription == "Transfer in");
        Assert.Contains(transfer.SubTypeDetails, x => x.SubTypeId == "12" && x.SubTypeDescription == "Transfer out");

        var disabled = response.Data.Single(x => x.TypeId == "999");
        Assert.Equal(string.Empty, disabled.TypeDescription);
        Assert.Equal(string.Empty, Assert.Single(disabled.SubTypeDetails).SubTypeDescription);
    }

    private static OkxRestApiResponse<List<T>> Deserialize<T>(params string[] fixturePath) where T : class
    {
        var json = FixtureReader.ReadManual(fixturePath);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
