namespace OKX.Api.Grid;

internal class OkxGridTriggerStrategyConverter(bool quotes) : BaseConverter<OkxGridTriggerStrategy>(quotes)
{
    public OkxGridTriggerStrategyConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridTriggerStrategy, string>> Mapping =>
    [
        new(OkxGridTriggerStrategy.Instant, "instant"),
        new(OkxGridTriggerStrategy.Price, "price"),
        new(OkxGridTriggerStrategy.RSI, "rsi"),
    ];
}