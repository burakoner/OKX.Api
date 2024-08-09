using OKX.Api.Grid.Enums;

namespace OKX.Api.Grid.Converters;

internal class OkxGridAlgoTriggerStrategyConverter(bool quotes) : BaseConverter<OkxGridAlgoTriggerStrategy>(quotes)
{
    public OkxGridAlgoTriggerStrategyConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridAlgoTriggerStrategy, string>> Mapping =>
    [
        new(OkxGridAlgoTriggerStrategy.Instant, "instant"),
        new(OkxGridAlgoTriggerStrategy.Price, "price"),
        new(OkxGridAlgoTriggerStrategy.Rsi, "rsi"),
    ];
}