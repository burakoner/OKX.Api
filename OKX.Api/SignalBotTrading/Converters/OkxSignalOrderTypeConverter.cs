using OKX.Api.SignalBotTrading.Enums;

namespace OKX.Api.SignalBotTrading.Converters;

internal class OkxSignalOrderTypeConverter(bool quotes) : BaseConverter<OkxSignalOrderType>(quotes)
{
    public OkxSignalOrderTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSignalOrderType, string>> Mapping =>
    [
        new(OkxSignalOrderType.LimitOrder, "1"),
        new(OkxSignalOrderType.MarketOrder, "2"),
        new(OkxSignalOrderType.TradingViewSignal, "9"),
    ];
}