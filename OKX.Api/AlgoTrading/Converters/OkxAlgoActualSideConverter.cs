using OKX.Api.AlgoTrading.Enums;

namespace OKX.Api.AlgoTrading.Converters;

internal class OkxAlgoActualSideConverter : BaseConverter<OkxAlgoActualSide>
{
    public OkxAlgoActualSideConverter() : this(true) { }
    public OkxAlgoActualSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAlgoActualSide, string>> Mapping =>
    [
        new(OkxAlgoActualSide.StopLoss, "sl"),
        new(OkxAlgoActualSide.TakeProfit, "tp"),
    ];
}