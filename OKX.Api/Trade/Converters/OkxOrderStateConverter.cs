using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

public class OkxOrderStateConverter(bool quotes) : BaseConverter<OkxOrderState>(quotes)
{
    public OkxOrderStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxOrderState, string>> Mapping =>
    [
        new(OkxOrderState.Live, "live"),
        new(OkxOrderState.Canceled, "canceled"),
        new(OkxOrderState.PartiallyFilled, "partially_filled"),
        new(OkxOrderState.Filled, "filled"),
        new(OkxOrderState.MmpCanceled, "mmp_canceled"),
    ];
}