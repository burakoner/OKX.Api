using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridAlgoSubOrderStatusConverter : BaseConverter<OkxGridAlgoSubOrderStatus>
{
    public OkxGridAlgoSubOrderStatusConverter() : this(true) { }
    public OkxGridAlgoSubOrderStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoSubOrderStatus, string>> Mapping =>
    [
        new(OkxGridAlgoSubOrderStatus.Live, "live"),
        new(OkxGridAlgoSubOrderStatus.Partial, "partially_filled"),
        new(OkxGridAlgoSubOrderStatus.Filled, "filled"),
        new(OkxGridAlgoSubOrderStatus.Cancelling, "cancelling"),
        new(OkxGridAlgoSubOrderStatus.Canceled, "canceled"),
    ];
}