using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridAlgoTriggerStrategyConverter : BaseConverter<OkxGridAlgoTriggerStrategy>
{
    public OkxGridAlgoTriggerStrategyConverter() : this(true) { }
    public OkxGridAlgoTriggerStrategyConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoTriggerStrategy, string>> Mapping =>
    [
        new(OkxGridAlgoTriggerStrategy.Instant, "instant"),
        new(OkxGridAlgoTriggerStrategy.Price, "price"),
        new(OkxGridAlgoTriggerStrategy.Rsi, "rsi"),
    ];
}