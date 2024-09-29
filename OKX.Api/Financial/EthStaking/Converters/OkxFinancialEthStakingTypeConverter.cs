namespace OKX.Api.Financial.EthStaking;

internal class OkxFinancialEthStakingTypeConverter(bool quotes) : BaseConverter<OkxFinancialEthStakingType>(quotes)
{
    public OkxFinancialEthStakingTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFinancialEthStakingType, string>> Mapping =>
    [
        new(OkxFinancialEthStakingType.Purchase, "purchase"),
        new(OkxFinancialEthStakingType.Redeem, "redeem"),
    ];
}