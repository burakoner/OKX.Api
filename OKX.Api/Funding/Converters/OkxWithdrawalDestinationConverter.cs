using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

public class OkxWithdrawalDestinationConverter(bool quotes) : BaseConverter<OkxWithdrawalDestination>(quotes)
{
    public OkxWithdrawalDestinationConverter() : this(true) { }

    protected override List<KeyValuePair<OkxWithdrawalDestination, string>> Mapping =>
    [
        new(OkxWithdrawalDestination.OKX, "3"),
        new(OkxWithdrawalDestination.DigitalCurrencyAddress, "4"),
    ];
}