using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class GridAlgoSubOrderStatusConverter : BaseConverter<OkxGridAlgoSubOrderStatus>
{
    public GridAlgoSubOrderStatusConverter() : this(true) { }
    public GridAlgoSubOrderStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoSubOrderStatus, string>> Mapping => new List<KeyValuePair<OkxGridAlgoSubOrderStatus, string>>
    {
        new KeyValuePair<OkxGridAlgoSubOrderStatus, string>(OkxGridAlgoSubOrderStatus.Live, "live"),
        new KeyValuePair<OkxGridAlgoSubOrderStatus, string>(OkxGridAlgoSubOrderStatus.Partial, "partially_filled"),
        new KeyValuePair<OkxGridAlgoSubOrderStatus, string>(OkxGridAlgoSubOrderStatus.Filled, "filled"),
        new KeyValuePair<OkxGridAlgoSubOrderStatus, string>(OkxGridAlgoSubOrderStatus.Cancelling, "cancelling"),
        new KeyValuePair<OkxGridAlgoSubOrderStatus, string>(OkxGridAlgoSubOrderStatus.Canceled, "canceled"),
    };
}