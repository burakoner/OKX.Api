namespace OKX.Api.Grid;

internal class OkxGridAlgoTriggerActionConverter(bool quotes) : BaseConverter<OkxGridAlgoTriggerAction>(quotes)
{
    public OkxGridAlgoTriggerActionConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridAlgoTriggerAction, string>> Mapping =>
    [
        new(OkxGridAlgoTriggerAction.Start, "start"),
        new(OkxGridAlgoTriggerAction.Stop, "stop"),
    ];
}