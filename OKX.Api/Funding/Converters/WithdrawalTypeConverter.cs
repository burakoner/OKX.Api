using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class WithdrawalTypeConverter : BaseConverter<OkxWithdrawalType>
{
    public WithdrawalTypeConverter() : this(true) { }
    public WithdrawalTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxWithdrawalType, string>> Mapping =>
    [
        new(OkxWithdrawalType.InternalDeposit, "3"),
        new(OkxWithdrawalType.BlockchainDeposit, "4"),
    ];
}