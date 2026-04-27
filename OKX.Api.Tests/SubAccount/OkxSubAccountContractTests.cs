using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Account;
using OKX.Api.Base;
using OKX.Api.SubAccount;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.SubAccount;

public class OkxSubAccountContractTests
{
    [Fact]
    public void FundingBalancesFixture_ParsesAllReturnedRows()
    {
        var response = DeserializeList<OkxSubAccountFundingBalance>("get-sub-account-funding-balances.json");

        Assert.Collection(response.Data!,
            first =>
            {
                Assert.Equal("BTC", first.Currency);
                Assert.Equal(0.50000000m, first.Balance);
                Assert.Equal(0.5m, first.AvailableBalance);
            },
            second =>
            {
                Assert.Equal("ETH", second.Currency);
                Assert.Equal(3m, second.AvailableBalance);
            });
    }

    [Fact]
    public void MaximumWithdrawalsFixture_ParsesEmptyStringExtendedFieldsAsNull()
    {
        var response = DeserializeList<OkxSubAccountMaximumWithdrawal>("get-sub-account-maximum-withdrawals.json");

        Assert.All(response.Data!, item =>
        {
            Assert.True(item.MaximumWithdrawal > 0);
            Assert.True(item.SpotOffsetMaximumWithdrawal > 0);
            Assert.Null(item.MaximumWithdrawalExtended);
            Assert.Null(item.SpotOffsetMaximumWithdrawalExtended);
        });
    }

    [Fact]
    public void TradingBalanceFixture_ParsesExpandedRiskAndCollateralFields()
    {
        var response = DeserializeList<OkxSubAccountTradingBalance>("get-sub-account-trading-balance.json");

        var balance = Assert.Single(response.Data!);
        Assert.Equal(624719833286m, balance.AvailableEquity);
        Assert.Equal(0m, balance.Delta);
        Assert.Equal(0m, balance.DeltaLeverage);
        Assert.Equal(OkxAccountDeltaNeutralStatus.Normal, balance.DeltaNeutralStatus);
        Assert.Equal(0m, balance.UnrealizedProfitAndLoss);
        Assert.Null(balance.MarginRatio);

        var detail = Assert.Single(balance.Details);
        Assert.Equal(OkxAccountAutoLendStatus.Off, detail.AutoLendStatus);
        Assert.Equal(OkxAccountForcedRepaymentType.NoFRP, detail.ForcedRepaymentType);
        Assert.False(detail.IsCollateralEnabled);
        Assert.False(detail.IsCollateralRestricted);
        Assert.Equal("0", detail.CollateralRestrictionStatus);
        Assert.Equal("0", detail.CollateralBorrowAutoConversion);
        Assert.Equal(0m, detail.FixedBalance);
        Assert.Equal(0m, detail.StrategyEquity);
        Assert.Equal(0m, detail.SpotCopyTradingEquity);
        Assert.Equal(0m, detail.IsolatedUnrealizedProfitAndLoss);
        Assert.Null(detail.MaxPossibleSpotRiskOffsetAmount);
        Assert.Null(detail.UserDefinedSpotRiskOffsetAmount);
        Assert.Null(detail.SpotInUseAmount);
        Assert.Null(detail.SpotAverageCostPrice);
        Assert.Null(detail.RewardBalance);
        Assert.Null(detail.SpotBalance);
        Assert.Null(detail.SpotUnrealizedProfitAndLoss);
        Assert.Null(detail.TotalProfitAndLoss);
    }

    [Fact]
    public void SetPermissionFixture_ParsesBooleanResponse()
    {
        var response = DeserializeList<OkxSubAccountPermissionOfTransferOut>("set-permission-of-transfer-out.json");

        Assert.Collection(response.Data!,
            first =>
            {
                Assert.Equal("Test001", first.SubAccountName);
                Assert.True(first.CanTransferOut);
            },
            second =>
            {
                Assert.Equal("Test002", second.SubAccountName);
                Assert.True(second.CanTransferOut);
            });
    }

    private static OkxRestApiResponse<List<T>> DeserializeList<T>(string fileName)
    {
        var json = FixtureReader.ReadManual("SubAccount", fileName);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
