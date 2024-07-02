using OKX.Api.AlgoTrading.Enums;

namespace OKX.Api.AlgoTrading.Converters;

internal class OkxAlgoOrderStateConverter(bool quotes) : BaseConverter<OkxAlgoOrderState>(quotes)
{
    public OkxAlgoOrderStateConverter() : this(true) { }

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