using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Trade;

public class OkxTradeOrderOperationContractTests
{
    [Fact]
    public void ManualRestPlaceOrderFixture_ParsesSubCode()
    {
        var response = DeserializeRest<OkxTradeOrderPlaceResponse>("Trade", "rest-place-order-with-subcode.json");

        var item = Assert.Single(response.Data!);
        Assert.Equal("51008", item.ErrorCode);
        Assert.Equal("Order failed", item.ErrorMessage);
        Assert.Equal("54070", item.SubCode);
    }

    [Fact]
    public void ManualRestAmendOrderFixture_ParsesSubCode()
    {
        var response = DeserializeRest<OkxTradeOrderAmend>("Trade", "rest-amend-order-with-subcode.json");

        var item = Assert.Single(response.Data!);
        Assert.Equal("req-01", item.RequestId);
        Assert.Equal("51008", item.ErrorCode);
        Assert.Equal("Amend failed", item.ErrorMessage);
        Assert.Equal("54071", item.SubCode);
    }

    [Fact]
    public void ManualWsPlaceOrderFixture_ParsesSubCode()
    {
        var items = DeserializeSocketData<OkxTradeOrderPlaceResponse>("Trade", "ws-place-order-with-subcode.json");

        var item = Assert.Single(items);
        Assert.Equal("51008", item.ErrorCode);
        Assert.Equal("Order failed", item.ErrorMessage);
        Assert.Equal("54070", item.SubCode);
    }

    [Fact]
    public void ManualWsAmendOrderFixture_ParsesSubCode()
    {
        var items = DeserializeSocketData<OkxTradeOrderAmend>("Trade", "ws-amend-order-with-subcode.json");

        var item = Assert.Single(items);
        Assert.Equal("req-ws-01", item.RequestId);
        Assert.Equal("51008", item.ErrorCode);
        Assert.Equal("Amend failed", item.ErrorMessage);
        Assert.Equal("54071", item.SubCode);
    }

    private static OkxRestApiResponse<List<T>> DeserializeRest<T>(params string[] fixturePath)
    {
        var json = FixtureReader.ReadManual(fixturePath);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }

    private static List<T> DeserializeSocketData<T>(params string[] fixturePath)
    {
        var json = FixtureReader.ReadManual(fixturePath);
        var token = JObject.Parse(json)["data"];

        Assert.NotNull(token);

        var response = token!.ToObject<List<T>>(JsonSerializer.Create(SerializerOptions.WithConverters));
        Assert.NotNull(response);
        return response!;
    }
}
