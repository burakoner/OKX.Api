using OKX.Api.SignalTrading.Enums;

namespace OKX.Api.SignalTrading.Converters;

internal class OkxSignalTpslTriggerPriceConverter(bool quotes) : BaseConverter<OkxSignalTpslTriggerPrice>(quotes)
{
    public OkxSignalTpslTriggerPriceConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSignalTpslTriggerPrice, string>> Mapping =>
    [
        new(OkxSignalTpslTriggerPrice.ProfitAndLossPercentage, "pnl"),
        new(OkxSignalTpslTriggerPrice.Price, "price"),
    ];
}