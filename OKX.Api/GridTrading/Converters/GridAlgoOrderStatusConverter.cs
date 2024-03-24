using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class GridAlgoOrderStatusConverter : BaseConverter<OkxGridAlgoOrderStatus>
{
    public GridAlgoOrderStatusConverter() : this(true) { }
    public GridAlgoOrderStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoOrderStatus, string>> Mapping => new List<KeyValuePair<OkxGridAlgoOrderStatus, string>>
    {
        new KeyValuePair<OkxGridAlgoOrderStatus, string>(OkxGridAlgoOrderStatus.Starting, "starting"),
        new KeyValuePair<OkxGridAlgoOrderStatus, string>(OkxGridAlgoOrderStatus.Running, "running"),
        new KeyValuePair<OkxGridAlgoOrderStatus, string>(OkxGridAlgoOrderStatus.Stopping, "stopping"),
        new KeyValuePair<OkxGridAlgoOrderStatus, string>(OkxGridAlgoOrderStatus.NoClosePosition, "no_close_position"),
    };
}