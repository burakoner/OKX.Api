namespace OKX.Api.Public.Converters;

internal class OkxInsuranceTypeConverter : BaseConverter<OkxInsuranceType>
{
    public OkxInsuranceTypeConverter() : this(true) { }
    public OkxInsuranceTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxInsuranceType, string>> Mapping =>
    [
        new(OkxInsuranceType.All, "all"),
        new(OkxInsuranceType.LiquidationBalanceDeposit, "liquidation_balance_deposit"),
        new(OkxInsuranceType.BankruptcyLoss, "bankruptcy_loss"),
        new(OkxInsuranceType.PlatformRevenue, "platform_revenue"),
    ];
}