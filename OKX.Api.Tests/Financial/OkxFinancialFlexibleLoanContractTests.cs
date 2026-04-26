using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Financial;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Financial;

public class OkxFinancialFlexibleLoanContractTests
{
    [Fact]
    public void ManualBorrowableCurrenciesFixture_ParsesCurrencies()
    {
        var response = DeserializeList<OkxFinancialFlexibleLoanBorrowableCurrency>("get-borrow-currencies.json");

        Assert.Equal(["USDT", "USDC"], response.Data!.Select(x => x.BorrowCurrency).ToArray());
    }

    [Fact]
    public void ManualCollateralAssetsFixture_ParsesAssetList()
    {
        var response = DeserializeList<OkxFinancialFlexibleLoanCollateralAssets>("get-collateral-assets.json");

        var container = Assert.Single(response.Data!);
        Assert.Equal(2, container.Assets.Count);
        Assert.Equal("BTC", container.Assets[0].Currency);
        Assert.Equal(1.7921483143067599m, container.Assets[0].Amount);
        Assert.Equal(158292.621793314105231m, container.Assets[0].NotionalUsd);
    }

    [Fact]
    public void ManualMaximumLoanFixture_ParsesQuotaFields()
    {
        var response = DeserializeList<OkxFinancialFlexibleLoanMaximumLoan>("post-max-loan.json");

        var maximumLoan = Assert.Single(response.Data!);
        Assert.Equal("USDT", maximumLoan.BorrowCurrency);
        Assert.Equal(820.12345678m, maximumLoan.MaximumLoanAmount);
        Assert.Equal(820.98765432m, maximumLoan.NotionalUsd);
        Assert.Equal(12000m, maximumLoan.RemainingQuota);
    }

    [Fact]
    public void ManualMaximumCollateralRedeemFixture_ParsesRedeemAmount()
    {
        var response = DeserializeList<OkxFinancialFlexibleLoanMaximumCollateralRedeemAmount>("get-max-collateral-redeem-amount.json");

        var redeemAmount = Assert.Single(response.Data!);
        Assert.Equal("BTC", redeemAmount.Currency);
        Assert.Equal(0.5789m, redeemAmount.MaximumRedeemAmount);
    }

    [Fact]
    public void ManualLoanInfoFixture_ParsesOrderIdAndNullableRiskWarningFields()
    {
        var response = DeserializeList<OkxFinancialFlexibleLoanInfo>("get-loan-info.json");

        var info = Assert.Single(response.Data!);
        Assert.Equal("12345", info.OrderId);
        Assert.Equal(0.8661285m, info.LoanNotionalUsd);
        Assert.Equal(1.5078763m, info.CollateralNotionalUsd);
        Assert.Equal(0.5742m, info.CurrentLtv);
        Assert.Equal(0.7374m, info.MarginCallLtv);
        Assert.Equal(0.8374m, info.LiquidationLtv);
        Assert.Equal("USDC", Assert.Single(info.LoanData).Currency);
        Assert.Null(info.RiskWarningData.LiquidationPrice);
        Assert.Equal(string.Empty, info.RiskWarningData.InstrumentId);
    }

    [Fact]
    public void ManualLoanHistoryFixture_ParsesDocumentedHistoryTypes()
    {
        var response = DeserializeList<OkxFinancialFlexibleLoanHistory>("get-loan-history.json");

        Assert.Equal(2, response.Data!.Count);
        Assert.Equal(OkxFinancialFlexibleLoanHistoryType.CollateralLocked, response.Data[0].Type);
        Assert.Equal(OkxFinancialFlexibleLoanHistoryType.BuyBorrowedCoin, response.Data[1].Type);
        Assert.Equal("17319133035195745", response.Data[1].ReferenceId);
    }

    [Fact]
    public void ManualInterestAccruedFixture_ParsesAccruedInterest()
    {
        var response = DeserializeList<OkxFinancialFlexibleLoanAccruedInterest>("get-interest-accrued.json");

        var interest = Assert.Single(response.Data!);
        Assert.Equal("USDC", interest.Currency);
        Assert.Equal(0.00004054m, interest.Interest);
        Assert.Equal(0.41m, interest.InterestRate);
        Assert.Equal(0.86599309m, interest.LoanAmount);
        Assert.Equal("17319133035195744", interest.ReferenceId);
        Assert.Equal(1731913200000L, interest.Timestamp);
    }

    private static OkxRestApiResponse<List<T>> DeserializeList<T>(params string[] fixturePath) where T : class
    {
        var json = FixtureReader.ReadManual(["Financial", "FlexibleLoan", .. fixturePath]);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
