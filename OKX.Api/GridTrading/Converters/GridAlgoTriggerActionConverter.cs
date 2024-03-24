using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class GridAlgoTriggerActionConverter : BaseConverter<OkxGridAlgoTriggerAction>
{
    public GridAlgoTriggerActionConverter() : this(true) { }
    public GridAlgoTriggerActionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoTriggerAction, string>> Mapping => new List<KeyValuePair<OkxGridAlgoTriggerAction, string>>
    {
        new KeyValuePair<OkxGridAlgoTriggerAction, string>(OkxGridAlgoTriggerAction.Start, "start"),
        new KeyValuePair<OkxGridAlgoTriggerAction, string>(OkxGridAlgoTriggerAction.Stop, "stop"),
    };
}