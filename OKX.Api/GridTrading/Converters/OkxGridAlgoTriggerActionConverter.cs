using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridAlgoTriggerActionConverter : BaseConverter<OkxGridAlgoTriggerAction>
{
    public OkxGridAlgoTriggerActionConverter() : this(true) { }
    public OkxGridAlgoTriggerActionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoTriggerAction, string>> Mapping =>
    [
        new(OkxGridAlgoTriggerAction.Start, "start"),
        new(OkxGridAlgoTriggerAction.Stop, "stop"),
    ];
}