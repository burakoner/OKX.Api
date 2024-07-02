using OKX.Api.AlgoTrading.Enums;

namespace OKX.Api.AlgoTrading.Converters;

internal class OkxAlgoOrderTypeConverter(bool quotes) : BaseConverter<OkxAlgoOrderType>(quotes)
{
    public OkxAlgoOrderTypeConverter() : this(true) { }

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