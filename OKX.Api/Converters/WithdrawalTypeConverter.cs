namespace OKX.Api.Converters;

internal class WithdrawalTypeConverter : BaseConverter<OkxWithdrawalType>
{
    public WithdrawalTypeConverter() : this(true) { }
    public WithdrawalTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxWithdrawalType, string>> Mapping => new List<KeyValuePair<OkxWithdrawalType, string>>
    {
        new KeyValuePair<OkxWithdrawalType, string>(OkxWithdrawalType.InternalDeposit, "3"),
        new KeyValuePair<OkxWithdrawalType, string>(OkxWithdrawalType.BlockchainDeposit, "4"),
    };
}