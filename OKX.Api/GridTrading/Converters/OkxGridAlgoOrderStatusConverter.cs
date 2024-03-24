using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridAlgoOrderStatusConverter : BaseConverter<OkxGridAlgoOrderStatus>
{
    public OkxGridAlgoOrderStatusConverter() : this(true) { }
    public OkxGridAlgoOrderStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoOrderStatus, string>> Mapping =>
    [
        new(OkxGridAlgoOrderStatus.Starting, "starting"),
        new(OkxGridAlgoOrderStatus.Running, "running"),
        new(OkxGridAlgoOrderStatus.Stopping, "stopping"),
        new(OkxGridAlgoOrderStatus.NoClosePosition, "no_close_position"),
    ];
}