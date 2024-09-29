namespace OKX.Api.Financial.OnChainEarn;

internal class OkxFinancialOnChainEarnOrderStateConverter(bool quotes) : BaseConverter<OkxFinancialOnChainEarnOrderState>(quotes)
{
    public OkxFinancialOnChainEarnOrderStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFinancialOnChainEarnOrderState, string>> Mapping =>
    [
        new(OkxFinancialOnChainEarnOrderState.Pending, "8"),
        new(OkxFinancialOnChainEarnOrderState.Cancelling, "13"),
        new(OkxFinancialOnChainEarnOrderState.Onchain, "9"),
        new(OkxFinancialOnChainEarnOrderState.Earning, "1"),
        new(OkxFinancialOnChainEarnOrderState.Redeeming, "2"),
    ];
}