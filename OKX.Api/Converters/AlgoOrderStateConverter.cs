namespace OKX.Api.Converters;

internal class AlgoOrderStateConverter : BaseConverter<OkxAlgoOrderState>
{
    public AlgoOrderStateConverter() : this(true) { }
    public AlgoOrderStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAlgoOrderState, string>> Mapping => new List<KeyValuePair<OkxAlgoOrderState, string>>
    {
        new KeyValuePair<OkxAlgoOrderState, string>(OkxAlgoOrderState.Live, "live"),
        new KeyValuePair<OkxAlgoOrderState, string>(OkxAlgoOrderState.Pause, "pause"),
        new KeyValuePair<OkxAlgoOrderState, string>(OkxAlgoOrderState.Effective, "effective"),
        new KeyValuePair<OkxAlgoOrderState, string>(OkxAlgoOrderState.PartiallyEffective, "partially_effective"),
        new KeyValuePair<OkxAlgoOrderState, string>(OkxAlgoOrderState.PartiallyFailed, "partially_failed"),
        new KeyValuePair<OkxAlgoOrderState, string>(OkxAlgoOrderState.Canceled, "canceled"),
        new KeyValuePair<OkxAlgoOrderState, string>(OkxAlgoOrderState.Failed, "order_failed"),
    };
}