using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxOrderStateConverter : BaseConverter<OkxOrderState>
{
    public OkxOrderStateConverter() : this(true) { }
    public OkxOrderStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxOrderState, string>> Mapping => new List<KeyValuePair<OkxOrderState, string>>
    {
        new KeyValuePair<OkxOrderState, string>(OkxOrderState.Live, "live"),
        new KeyValuePair<OkxOrderState, string>(OkxOrderState.Canceled, "canceled"),
        new KeyValuePair<OkxOrderState, string>(OkxOrderState.PartiallyFilled, "partially_filled"),
        new KeyValuePair<OkxOrderState, string>(OkxOrderState.Filled, "filled"),
        new KeyValuePair<OkxOrderState, string>(OkxOrderState.MmpCanceled, "mmp_canceled"),
    };
}