using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Block;
using OKX.Api.Common;
using OKX.Api.Trade;
using System.Reflection;

namespace OKX.Api.Tests.Block;

public class OkxBlockRestRequestContractTests
{
    [Fact]
    public void CreateRfqRequestParameters_SerializesAccountAllocationsAsArray()
    {
        var parameters = InvokeCreateRfqParameters(
            counterparties: ["Trader1", "Trader2"],
            legs:
            [
                new OkxBlockLegRequest
                {
                    InstrumentId = "BTC-USDT-SWAP",
                    Size = "25",
                    Side = OkxTradeOrderSide.Buy
                }
            ],
            clientRfqId: "rfq01",
            anonymous: true,
            allowPartialExecution: false,
            accountAllocations:
            [
                new OkxBlockAllocationRequest
                {
                    Account = "sub-1",
                    Legs =
                    [
                        new OkxBlockAllocationLegRequest
                        {
                            InstrumentId = "BTC-USDT-SWAP",
                            Size = "10",
                            TradeMode = OkxTradeMode.Cross
                        }
                    ]
                }
            ],
            tag: "rfq-tag");

        var payload = SerializeParameters(parameters);

        Assert.Equal("rfq01", payload["clRfqId"]?.Value<string>());
        Assert.Equal("rfq-tag", payload["tag"]?.Value<string>());
        Assert.False(payload["allowPartialExecution"]!.Value<bool>());

        var accountAllocations = Assert.IsAssignableFrom<JArray>(payload["acctAlloc"]);
        var allocation = Assert.Single(accountAllocations.Values<JObject>());
        Assert.NotNull(allocation);
        Assert.Equal("sub-1", allocation["acct"]?.Value<string>());

        var allocationLeg = Assert.Single(allocation["legs"]!.Values<JObject>());
        Assert.NotNull(allocationLeg);
        Assert.Equal("BTC-USDT-SWAP", allocationLeg["instId"]?.Value<string>());
        Assert.Equal("10", allocationLeg["sz"]?.Value<string>());
        Assert.Equal("cross", allocationLeg["tdMode"]?.Value<string>());
    }

    [Fact]
    public void CreateQuoteRequestParameters_UsesClQuoteIdField()
    {
        var parameters = InvokeCreateQuoteParameters(
            rfqId: "22539",
            quoteSide: OkxTradeOrderSide.Buy,
            legs:
            [
                new OkxBlockLegQuote
                {
                    InstrumentId = "BTC-USDT-SWAP",
                    Price = "39450.0",
                    Size = "200000",
                    Side = OkxTradeOrderSide.Buy
                }
            ],
            clientQuoteId: "q001",
            anonymous: true,
            expiresIn: 30,
            tag: "quote-tag");

        var payload = SerializeParameters(parameters);

        Assert.Equal("22539", payload["rfqId"]?.Value<string>());
        Assert.Equal("q001", payload["clQuoteId"]?.Value<string>());
        Assert.Null(payload["clRfqId"]);
        Assert.Equal("quote-tag", payload["tag"]?.Value<string>());
        Assert.Equal("buy", payload["quoteSide"]?.Value<string>());
        Assert.Equal("30", payload["expiresIn"]?.Value<string>());
    }

    [Fact]
    public void QuoteProductRequest_SerializesIncludeAllAndDerivativeInstFamily()
    {
        var request = new OkxBlockQuoteProductRequest
        {
            InstrumentType = OkxInstrumentType.Swap,
            IncludeAll = true,
            Data =
            [
                new OkxBlockQuoteProductRequestData
                {
                    InstrumentFamily = "BTC-USDT",
                    MaximumBlockSize = "10000",
                    MakerPriceBand = "5"
                }
            ]
        };

        var payload = JObject.Parse(JsonConvert.SerializeObject(request, SerializerOptions.WithConverters));

        Assert.Equal("SWAP", payload["instType"]?.Value<string>());
        Assert.True(payload["includeAll"]!.Value<bool>());

        var entry = Assert.Single(payload["data"]!.Values<JObject>());
        Assert.NotNull(entry);
        Assert.Equal("BTC-USDT", entry["instFamily"]?.Value<string>());
        Assert.Equal("10000", entry["maxBlockSz"]?.Value<string>());
        Assert.Equal("5", entry["makerPxBand"]?.Value<string>());
        Assert.Null(entry["instId"]);
    }

    private static ParameterCollection InvokeCreateRfqParameters(
        IEnumerable<string> counterparties,
        IEnumerable<OkxBlockLegRequest> legs,
        string? clientRfqId,
        bool anonymous,
        bool allowPartialExecution,
        IEnumerable<OkxBlockAllocationRequest>? accountAllocations,
        string? tag)
    {
        var method = typeof(OkxBlockRestClient).GetMethod("CreateRfqRequestParameters", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);
        return (ParameterCollection)method!.Invoke(null, [counterparties, legs, clientRfqId, anonymous, allowPartialExecution, accountAllocations, tag])!;
    }

    private static ParameterCollection InvokeCreateQuoteParameters(
        string rfqId,
        OkxTradeOrderSide quoteSide,
        IEnumerable<OkxBlockLegQuote> legs,
        string? clientQuoteId,
        bool anonymous,
        int? expiresIn,
        string? tag)
    {
        var method = typeof(OkxBlockRestClient).GetMethod("CreateQuoteRequestParameters", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);
        return (ParameterCollection)method!.Invoke(null, [rfqId, quoteSide, legs, clientQuoteId, anonymous, expiresIn, tag])!;
    }

    private static JObject SerializeParameters(ParameterCollection parameters)
        => JObject.Parse(JsonConvert.SerializeObject(parameters, SerializerOptions.WithConverters));
}
