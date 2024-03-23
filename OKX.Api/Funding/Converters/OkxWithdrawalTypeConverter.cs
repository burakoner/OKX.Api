using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxWithdrawalTypeConverter : BaseConverter<OkxWithdrawalType>
{
    public OkxWithdrawalTypeConverter() : this(true) { }
    public OkxWithdrawalTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxWithdrawalType, string>> Mapping =>
    [
        new(OkxWithdrawalType.InternalDeposit, "3"),
        new(OkxWithdrawalType.BlockchainDeposit, "4"),
    ];
}