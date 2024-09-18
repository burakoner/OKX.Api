using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxTradeOrderStateConverter(bool quotes) : BaseConverter<OkxTradeOrderState>(quotes)
{
    public OkxTradeOrderStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradeOrderState, string>> Mapping =>
    [
        new(OkxTradeOrderState.Live, "live"),
        new(OkxTradeOrderState.Canceled, "canceled"),
        new(OkxTradeOrderState.PartiallyFilled, "partially_filled"),
        new(OkxTradeOrderState.Filled, "filled"),
        new(OkxTradeOrderState.MmpCanceled, "mmp_canceled"),
    ];
}