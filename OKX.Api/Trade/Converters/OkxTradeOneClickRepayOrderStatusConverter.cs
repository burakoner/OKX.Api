using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxTradeOneClickRepayOrderStatusConverter(bool quotes) : BaseConverter<OkxTradeOneClickRepayOrderStatus>(quotes)
{
    public OkxTradeOneClickRepayOrderStatusConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradeOneClickRepayOrderStatus, string>> Mapping =>
    [
        new(OkxTradeOneClickRepayOrderStatus.Running, "running"),
        new(OkxTradeOneClickRepayOrderStatus.Filled, "filled"),
        new(OkxTradeOneClickRepayOrderStatus.Failed, "failed"),
    ];
}