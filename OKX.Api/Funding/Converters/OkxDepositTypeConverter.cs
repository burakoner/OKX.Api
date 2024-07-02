using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxDepositTypeConverter(bool quotes) : BaseConverter<OkxDepositType>(quotes)
{
    public OkxDepositTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxDepositType, string>> Mapping =>
    [
        new(OkxDepositType.InternalDeposit, "3"),
        new(OkxDepositType.BlockchainDeposit, "4"),
    ];
}