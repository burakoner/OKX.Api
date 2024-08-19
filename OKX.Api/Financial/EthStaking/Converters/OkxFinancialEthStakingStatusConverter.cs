using OKX.Api.Financial.EthStaking.Enums;

namespace OKX.Api.Financial.EthStaking.Converters;

internal class OkxFinancialEthStakingStatusConverter(bool quotes) : BaseConverter<OkxFinancialEthStakingStatus>(quotes)
{
    public OkxFinancialEthStakingStatusConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFinancialEthStakingStatus, string>> Mapping =>
    [
        new(OkxFinancialEthStakingStatus.Pending, "pending"),
        new(OkxFinancialEthStakingStatus.Success, "success"),
        new(OkxFinancialEthStakingStatus.Failed, "failed"),
    ];
}