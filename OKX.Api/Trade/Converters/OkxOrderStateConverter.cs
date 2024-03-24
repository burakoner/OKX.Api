using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxOrderStateConverter : BaseConverter<OkxOrderState>
{
    public OkxOrderStateConverter() : this(true) { }
    public OkxOrderStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxOrderState, string>> Mapping =>
    [
        new(OkxOrderState.Live, "live"),
        new(OkxOrderState.Canceled, "canceled"),
        new(OkxOrderState.PartiallyFilled, "partially_filled"),
        new(OkxOrderState.Filled, "filled"),
        new(OkxOrderState.MmpCanceled, "mmp_canceled"),
    ];
}