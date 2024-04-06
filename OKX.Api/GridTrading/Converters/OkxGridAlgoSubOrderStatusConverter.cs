using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

public class OkxGridAlgoSubOrderStatusConverter(bool quotes) : BaseConverter<OkxGridAlgoSubOrderStatus>(quotes)
{
    public OkxGridAlgoSubOrderStatusConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridAlgoSubOrderStatus, string>> Mapping =>
    [
        new(OkxGridAlgoSubOrderStatus.Live, "live"),
        new(OkxGridAlgoSubOrderStatus.Partial, "partially_filled"),
        new(OkxGridAlgoSubOrderStatus.Filled, "filled"),
        new(OkxGridAlgoSubOrderStatus.Cancelling, "cancelling"),
        new(OkxGridAlgoSubOrderStatus.Canceled, "canceled"),
    ];
}