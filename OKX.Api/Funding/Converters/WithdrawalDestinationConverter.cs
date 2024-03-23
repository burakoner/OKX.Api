using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class WithdrawalDestinationConverter : BaseConverter<OkxWithdrawalDestination>
{
    public WithdrawalDestinationConverter() : this(true) { }
    public WithdrawalDestinationConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxWithdrawalDestination, string>> Mapping =>
    [
        new(OkxWithdrawalDestination.OKX, "3"),
        new(OkxWithdrawalDestination.DigitalCurrencyAddress, "4"),

    ];
}