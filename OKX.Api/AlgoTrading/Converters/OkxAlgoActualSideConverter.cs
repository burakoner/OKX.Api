using OKX.Api.AlgoTrading.Enums;

namespace OKX.Api.AlgoTrading.Converters;

public class OkxAlgoActualSideConverter(bool quotes) : BaseConverter<OkxAlgoActualSide>(quotes)
{
    public OkxAlgoActualSideConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAlgoActualSide, string>> Mapping =>
    [
        new(OkxAlgoActualSide.StopLoss, "sl"),
        new(OkxAlgoActualSide.TakeProfit, "tp"),
    ];
}