using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Account;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Trade;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Account;

public class OkxAccountEventContractContractTests
{
    [Fact]
    public void ManualEventFeeRatesFixture_ParsesSettlementFeeRate()
    {
        var response = DeserializeRest<OkxAccountFeeRate>("Account", "get-fee-rates-events.json");

        var feeRate = Assert.Single(response.Data!);
        Assert.Equal(OkxInstrumentType.Events, feeRate.InstrumentType);
        Assert.Equal(0.0004m, feeRate.Settlement);
        Assert.Equal("1", Assert.Single(feeRate.FeeGroup).GroupId);
    }

    [Fact]
    public void ManualEventBillsFixture_ParsesNewEventBillSubTypes()
    {
        var response = DeserializeRest<OkxAccountBill>("Account", "event-bills.json");

        Assert.Contains(response.Data!, x => x.BillSubType == OkxAccountBillSubType.EventBuyYes);
        Assert.Contains(response.Data!, x => x.BillSubType == OkxAccountBillSubType.EventYesExpiry);
    }

    [Fact]
    public void ManualEventTransactionsFixture_ParsesNewEventTransactionSubTypes()
    {
        var response = DeserializeRest<OkxTradeTransaction>("Account", "event-transactions.json");

        Assert.Contains(response.Data!, x => x.TransactionType == OkxAccountBillSubType.EventBuyNo);
        Assert.Contains(response.Data!, x => x.TransactionType == OkxAccountBillSubType.EventNoExpiry);
    }

    private static OkxRestApiResponse<List<T>> DeserializeRest<T>(params string[] fixturePath) where T : class
    {
        var json = FixtureReader.ReadManual(fixturePath);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
