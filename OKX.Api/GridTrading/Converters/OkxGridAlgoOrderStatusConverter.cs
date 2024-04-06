using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

public class OkxGridAlgoOrderStatusConverter(bool quotes) : BaseConverter<OkxGridAlgoOrderStatus>(quotes)
{
    public OkxGridAlgoOrderStatusConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridAlgoOrderStatus, string>> Mapping =>
    [
        new(OkxGridAlgoOrderStatus.Starting, "starting"),
        new(OkxGridAlgoOrderStatus.Running, "running"),
        new(OkxGridAlgoOrderStatus.Stopping, "stopping"),
        new(OkxGridAlgoOrderStatus.NoClosePosition, "no_close_position"),
    ];
}