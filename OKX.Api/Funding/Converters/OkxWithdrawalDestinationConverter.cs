using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxWithdrawalDestinationConverter : BaseConverter<OkxWithdrawalDestination>
{
    public OkxWithdrawalDestinationConverter() : this(true) { }
    public OkxWithdrawalDestinationConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxWithdrawalDestination, string>> Mapping =>
    [
        new(OkxWithdrawalDestination.OKX, "3"),
        new(OkxWithdrawalDestination.DigitalCurrencyAddress, "4"),
    ];
}