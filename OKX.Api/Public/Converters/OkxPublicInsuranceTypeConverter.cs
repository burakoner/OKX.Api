namespace OKX.Api.Public;

internal class OkxPublicInsuranceTypeConverter(bool quotes) : BaseConverter<OkxPublicInsuranceType>(quotes)
{
    public OkxPublicInsuranceTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxPublicInsuranceType, string>> Mapping =>
    [
        new(OkxPublicInsuranceType.All, "all"),
        new(OkxPublicInsuranceType.LiquidationBalanceDeposit, "liquidation_balance_deposit"),
        new(OkxPublicInsuranceType.BankruptcyLoss, "bankruptcy_loss"),
        new(OkxPublicInsuranceType.PlatformRevenue, "platform_revenue"),
    ];
}