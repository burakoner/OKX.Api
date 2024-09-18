using OKX.Api.Grid.Enums;

namespace OKX.Api.Grid.Converters;

internal class OkxGridTriggerStrategyConverter(bool quotes) : BaseConverter<OkxGridTriggerStrategy>(quotes)
{
    public OkxGridTriggerStrategyConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridTriggerStrategy, string>> Mapping =>
    [
        new(OkxGridTriggerStrategy.Instant, "instant"),
        new(OkxGridTriggerStrategy.Price, "price"),
        new(OkxGridTriggerStrategy.Rsi, "rsi"),
    ];
}