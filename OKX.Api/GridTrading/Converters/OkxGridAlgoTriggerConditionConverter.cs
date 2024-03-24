using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridAlgoTriggerConditionConverter(bool quotes) : BaseConverter<OkxGridAlgoTriggerCondition>(quotes)
{
    public OkxGridAlgoTriggerConditionConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridAlgoTriggerCondition, string>> Mapping =>
    [
        new(OkxGridAlgoTriggerCondition.CrossUp, "cross_up"),
        new(OkxGridAlgoTriggerCondition.CrossDown, "cross_down"),
        new(OkxGridAlgoTriggerCondition.Above, "above"),
        new(OkxGridAlgoTriggerCondition.Below, "below"),
        new(OkxGridAlgoTriggerCondition.Cross, "cross"),
    ];
}