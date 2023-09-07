namespace OKX.Api.Converters;

internal class OrderStateConverter : BaseConverter<OkxOrderState>
{
    public OrderStateConverter() : this(true) { }
    public OrderStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxOrderState, string>> Mapping => new List<KeyValuePair<OkxOrderState, string>>
    {
        new KeyValuePair<OkxOrderState, string>(OkxOrderState.Live, "live"),
        new KeyValuePair<OkxOrderState, string>(OkxOrderState.Canceled, "canceled"),
        new KeyValuePair<OkxOrderState, string>(OkxOrderState.PartiallyFilled, "partially_filled"),
        new KeyValuePair<OkxOrderState, string>(OkxOrderState.Filled, "filled"),
        new KeyValuePair<OkxOrderState, string>(OkxOrderState.MmpCanceled, "mmp_canceled"),
    };
}