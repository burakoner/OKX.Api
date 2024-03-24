using OKX.Api.GridTrade.Enums;

namespace OKX.Api.GridTrade.Converters;

internal class GridAlgoTriggerStrategyConverter : BaseConverter<OkxGridAlgoTriggerStrategy>
{
    public GridAlgoTriggerStrategyConverter() : this(true) { }
    public GridAlgoTriggerStrategyConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoTriggerStrategy, string>> Mapping => new List<KeyValuePair<OkxGridAlgoTriggerStrategy, string>>
    {
        new KeyValuePair<OkxGridAlgoTriggerStrategy, string>(OkxGridAlgoTriggerStrategy.Instant, "instant"),
        new KeyValuePair<OkxGridAlgoTriggerStrategy, string>(OkxGridAlgoTriggerStrategy.Price, "price"),
        new KeyValuePair<OkxGridAlgoTriggerStrategy, string>(OkxGridAlgoTriggerStrategy.Rsi, "rsi"),
    };
}