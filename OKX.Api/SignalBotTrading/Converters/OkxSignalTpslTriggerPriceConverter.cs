using OKX.Api.SignalBotTrading.Enums;

namespace OKX.Api.SignalBotTrading.Converters;

internal class OkxSignalTpslTriggerPriceConverter(bool quotes) : BaseConverter<OkxSignalTpslTriggerPrice>(quotes)
{
    public OkxSignalTpslTriggerPriceConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSignalTpslTriggerPrice, string>> Mapping =>
    [
        new(OkxSignalTpslTriggerPrice.ProfitAndLossPercentage, "pnl"),
        new(OkxSignalTpslTriggerPrice.Price, "price"),
    ];
}