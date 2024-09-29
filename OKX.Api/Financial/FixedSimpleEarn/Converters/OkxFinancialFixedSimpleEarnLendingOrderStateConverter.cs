namespace OKX.Api.Financial.FixedSimpleEarn;

internal class OkxFinancialFixedSimpleEarnLendingOrderStateConverter(bool quotes) : BaseConverter<OkxFinancialFixedSimpleEarnLendingOrderState>(quotes)
{
    public OkxFinancialFixedSimpleEarnLendingOrderStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFinancialFixedSimpleEarnLendingOrderState, string>> Mapping =>
    [
        new(OkxFinancialFixedSimpleEarnLendingOrderState.Pending, "pending"),
        new(OkxFinancialFixedSimpleEarnLendingOrderState.Earning, "earning"),
        new(OkxFinancialFixedSimpleEarnLendingOrderState.Expired, "expired"),
        new(OkxFinancialFixedSimpleEarnLendingOrderState.Settled, "settled"),
    ];
}