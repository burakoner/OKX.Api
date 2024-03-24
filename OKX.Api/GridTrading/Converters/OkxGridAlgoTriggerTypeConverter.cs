using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridAlgoTriggerTypeConverter : BaseConverter<OkxGridAlgoTriggerType>
{
    public OkxGridAlgoTriggerTypeConverter() : this(true) { }
    public OkxGridAlgoTriggerTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoTriggerType, string>> Mapping =>
    [
        new(OkxGridAlgoTriggerType.Auto, "auto"),
        new(OkxGridAlgoTriggerType.Manual, "manual"),
    ];
}