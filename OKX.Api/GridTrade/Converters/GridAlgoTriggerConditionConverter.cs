using OKX.Api.GridTrade.Enums;

namespace OKX.Api.GridTrade.Converters;

internal class GridAlgoTriggerConditionConverter : BaseConverter<OkxGridAlgoTriggerCondition>
{
    public GridAlgoTriggerConditionConverter() : this(true) { }
    public GridAlgoTriggerConditionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoTriggerCondition, string>> Mapping => new List<KeyValuePair<OkxGridAlgoTriggerCondition, string>>
    {
        new KeyValuePair<OkxGridAlgoTriggerCondition, string>(OkxGridAlgoTriggerCondition.CrossUp, "cross_up"),
        new KeyValuePair<OkxGridAlgoTriggerCondition, string>(OkxGridAlgoTriggerCondition.CrossDown, "cross_down"),
        new KeyValuePair<OkxGridAlgoTriggerCondition, string>(OkxGridAlgoTriggerCondition.Above, "above"),
        new KeyValuePair<OkxGridAlgoTriggerCondition, string>(OkxGridAlgoTriggerCondition.Below, "below"),
        new KeyValuePair<OkxGridAlgoTriggerCondition, string>(OkxGridAlgoTriggerCondition.Cross, "cross"),
    };
}