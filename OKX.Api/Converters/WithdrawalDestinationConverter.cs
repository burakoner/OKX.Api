namespace OKX.Api.Converters;

internal class WithdrawalDestinationConverter : BaseConverter<OkxWithdrawalDestination>
{
    public WithdrawalDestinationConverter() : this(true) { }
    public WithdrawalDestinationConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxWithdrawalDestination, string>> Mapping => new List<KeyValuePair<OkxWithdrawalDestination, string>>
    {
        new KeyValuePair<OkxWithdrawalDestination, string>(OkxWithdrawalDestination.OKX, "3"),
        new KeyValuePair<OkxWithdrawalDestination, string>(OkxWithdrawalDestination.DigitalCurrencyAddress, "4"),

    };
}