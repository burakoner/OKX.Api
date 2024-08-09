using OKX.Api.Grid.Enums;

namespace OKX.Api.Grid.Converters;

internal class OkxGridAlgoTriggerTypeConverter(bool quotes) : BaseConverter<OkxGridAlgoTriggerType>(quotes)
{
    public OkxGridAlgoTriggerTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridAlgoTriggerType, string>> Mapping =>
    [
        new(OkxGridAlgoTriggerType.Auto, "auto"),
        new(OkxGridAlgoTriggerType.Manual, "manual"),
    ];
}