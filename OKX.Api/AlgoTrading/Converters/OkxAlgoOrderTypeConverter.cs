using OKX.Api.AlgoTrading.Enums;

namespace OKX.Api.AlgoTrading.Converters;

internal class OkxAlgoOrderTypeConverter : BaseConverter<OkxAlgoOrderType>
{
    public OkxAlgoOrderTypeConverter() : this(true) { }
    public OkxAlgoOrderTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAlgoOrderType, string>> Mapping =>
    [
        new(OkxAlgoOrderType.Conditional, "conditional"),
        new(OkxAlgoOrderType.OCO, "oco"),
        new(OkxAlgoOrderType.Trigger, "trigger"),
        new(OkxAlgoOrderType.TrailingOrder, "move_order_stop"),
        new(OkxAlgoOrderType.Iceberg, "iceberg"),
        new(OkxAlgoOrderType.TWAP, "twap"),
    ];
}