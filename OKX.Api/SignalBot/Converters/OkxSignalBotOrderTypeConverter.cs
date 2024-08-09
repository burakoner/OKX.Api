using OKX.Api.SignalBot.Enums;

namespace OKX.Api.SignalBot.Converters;

internal class OkxSignalBotOrderTypeConverter(bool quotes) : BaseConverter<OkxSignalBotOrderType>(quotes)
{
    public OkxSignalBotOrderTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSignalBotOrderType, string>> Mapping =>
    [
        new(OkxSignalBotOrderType.LimitOrder, "1"),
        new(OkxSignalBotOrderType.MarketOrder, "2"),
        new(OkxSignalBotOrderType.TradingViewSignal, "9"),
    ];
}