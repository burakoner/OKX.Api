using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

public class OkxWithdrawalTypeConverter(bool quotes) : BaseConverter<OkxWithdrawalType>(quotes)
{
    public OkxWithdrawalTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxWithdrawalType, string>> Mapping =>
    [
        new(OkxWithdrawalType.InternalDeposit, "3"),
        new(OkxWithdrawalType.BlockchainDeposit, "4"),
    ];
}