using OKX.Api.Spread.Enums;

namespace OKX.Api.Spread.Converters;

internal class OkxSpreadOrderStateConverter(bool quotes) : BaseConverter<OkxSpreadOrderState>(quotes)
{
    public OkxSpreadOrderStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSpreadOrderState, string>> Mapping =>
    [
        new(OkxSpreadOrderState.Live, "live"),
        new(OkxSpreadOrderState.Filled, "filled"),
        new(OkxSpreadOrderState.PartiallyFilled, "partially_filled"),
        new(OkxSpreadOrderState.Canceled, "canceled"),
    ];
}