using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridAlgoTriggerConditionConverter : BaseConverter<OkxGridAlgoTriggerCondition>
{
    public OkxGridAlgoTriggerConditionConverter() : this(true) { }
    public OkxGridAlgoTriggerConditionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoTriggerCondition, string>> Mapping =>
    [
        new(OkxGridAlgoTriggerCondition.CrossUp, "cross_up"),
        new(OkxGridAlgoTriggerCondition.CrossDown, "cross_down"),
        new(OkxGridAlgoTriggerCondition.Above, "above"),
        new(OkxGridAlgoTriggerCondition.Below, "below"),
        new(OkxGridAlgoTriggerCondition.Cross, "cross"),
    ];
}