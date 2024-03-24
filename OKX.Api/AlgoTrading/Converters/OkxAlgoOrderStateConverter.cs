using OKX.Api.AlgoTrading.Enums;

namespace OKX.Api.AlgoTrading.Converters;

internal class OkxAlgoOrderStateConverter : BaseConverter<OkxAlgoOrderState>
{
    public OkxAlgoOrderStateConverter() : this(true) { }
    public OkxAlgoOrderStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAlgoOrderState, string>> Mapping =>
    [
        new(OkxAlgoOrderState.Live, "live"),
        new(OkxAlgoOrderState.Pause, "pause"),
        new(OkxAlgoOrderState.Effective, "effective"),
        new(OkxAlgoOrderState.PartiallyEffective, "partially_effective"),
        new(OkxAlgoOrderState.PartiallyFailed, "partially_failed"),
        new(OkxAlgoOrderState.Canceled, "canceled"),
        new(OkxAlgoOrderState.Failed, "order_failed"),
    ];
}