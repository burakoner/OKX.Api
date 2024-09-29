namespace OKX.Api.SignalBot;

internal class OkxSignalBotTriggerPriceConverter(bool quotes) : BaseConverter<OkxSignalBotTriggerPrice>(quotes)
{
    public OkxSignalBotTriggerPriceConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSignalBotTriggerPrice, string>> Mapping =>
    [
        new(OkxSignalBotTriggerPrice.ProfitAndLossPercentage, "pnl"),
        new(OkxSignalBotTriggerPrice.Price, "price"),
    ];
}