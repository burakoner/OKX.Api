namespace OKX.Api.Converters;

internal class AlgoOrderTypeConverter : BaseConverter<OkxAlgoOrderType>
{
    public AlgoOrderTypeConverter() : this(true) { }
    public AlgoOrderTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAlgoOrderType, string>> Mapping => new List<KeyValuePair<OkxAlgoOrderType, string>>
    {
        new KeyValuePair<OkxAlgoOrderType, string>(OkxAlgoOrderType.Conditional, "conditional"),
        new KeyValuePair<OkxAlgoOrderType, string>(OkxAlgoOrderType.OCO, "oco"),
        new KeyValuePair<OkxAlgoOrderType, string>(OkxAlgoOrderType.Trigger, "trigger"),
        new KeyValuePair<OkxAlgoOrderType, string>(OkxAlgoOrderType.TrailingOrder, "move_order_stop"),
        new KeyValuePair<OkxAlgoOrderType, string>(OkxAlgoOrderType.Iceberg, "iceberg"),
        new KeyValuePair<OkxAlgoOrderType, string>(OkxAlgoOrderType.TWAP, "twap"),
    };
}