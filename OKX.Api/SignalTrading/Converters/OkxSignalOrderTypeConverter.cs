using OKX.Api.SignalTrading.Enums;

namespace OKX.Api.SignalTrading.Converters;

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