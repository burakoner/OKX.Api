using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxFundingDepositTypeConverter(bool quotes) : BaseConverter<OkxFundingDepositType>(quotes)
{
    public OkxFundingDepositTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFundingDepositType, string>> Mapping =>
    [
        new(OkxFundingDepositType.InternalDeposit, "3"),
        new(OkxFundingDepositType.BlockchainDeposit, "4"),
    ];
}