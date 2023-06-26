namespace OKX.Api.Converters;

internal class GridAlgoTriggerTypeConverter : BaseConverter<OkxGridAlgoTriggerType>
{
    public GridAlgoTriggerTypeConverter() : this(true) { }
    public GridAlgoTriggerTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoTriggerType, string>> Mapping => new List<KeyValuePair<OkxGridAlgoTriggerType, string>>
    {
        new KeyValuePair<OkxGridAlgoTriggerType, string>(OkxGridAlgoTriggerType.Auto, "auto"),
        new KeyValuePair<OkxGridAlgoTriggerType, string>(OkxGridAlgoTriggerType.Manual, "manual"),
    };
}