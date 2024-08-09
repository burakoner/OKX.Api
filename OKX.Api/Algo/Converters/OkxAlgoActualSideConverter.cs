using OKX.Api.Algo.Enums;

namespace OKX.Api.Algo.Converters;

internal class OkxAlgoActualSideConverter(bool quotes) : BaseConverter<OkxAlgoActualSide>(quotes)
{
    public OkxAlgoActualSideConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAlgoActualSide, string>> Mapping =>
    [
        new(OkxAlgoActualSide.StopLoss, "sl"),
        new(OkxAlgoActualSide.TakeProfit, "tp"),
    ];
}