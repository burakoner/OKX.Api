using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxTradeEasyConvertOrderStatusConverter(bool quotes) : BaseConverter<OkxTradeEasyConvertOrderStatus>(quotes)
{
    public OkxTradeEasyConvertOrderStatusConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradeEasyConvertOrderStatus, string>> Mapping =>
    [
        new(OkxTradeEasyConvertOrderStatus.Running, "running"),
        new(OkxTradeEasyConvertOrderStatus.Filled, "filled"),
        new(OkxTradeEasyConvertOrderStatus.Failed, "failed"),
    ];
}