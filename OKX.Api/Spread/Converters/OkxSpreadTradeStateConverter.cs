using OKX.Api.Spread.Enums;

namespace OKX.Api.Spread.Converters;

internal class OkxSpreadTradeStateConverter(bool quotes) : BaseConverter<OkxSpreadTradeState>(quotes)
{
    public OkxSpreadTradeStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSpreadTradeState, string>> Mapping =>
    [
        new(OkxSpreadTradeState.Filled, "filled"),
        new(OkxSpreadTradeState.Rejected, "rejected"),
    ];
}