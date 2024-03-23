using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxDepositTypeConverter : BaseConverter<OkxDepositType>
{
    public OkxDepositTypeConverter() : this(true) { }
    public OkxDepositTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxDepositType, string>> Mapping =>
    [
        new(OkxDepositType.InternalDeposit, "3"),
        new(OkxDepositType.BlockchainDeposit, "4"),
    ];
}