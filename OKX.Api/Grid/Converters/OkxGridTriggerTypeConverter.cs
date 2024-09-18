using OKX.Api.Grid.Enums;

namespace OKX.Api.Grid.Converters;

internal class OkxGridTriggerTypeConverter(bool quotes) : BaseConverter<OkxGridTriggerType>(quotes)
{
    public OkxGridTriggerTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridTriggerType, string>> Mapping =>
    [
        new(OkxGridTriggerType.Auto, "auto"),
        new(OkxGridTriggerType.Manual, "manual"),
    ];
}