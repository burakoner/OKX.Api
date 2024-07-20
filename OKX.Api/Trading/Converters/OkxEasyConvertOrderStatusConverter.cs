using OKX.Api.Trading.Enums;

namespace OKX.Api.Trading.Converters;

internal class OkxEasyConvertOrderStatusConverter(bool quotes) : BaseConverter<OkxEasyConvertOrderStatus>(quotes)
{
    public OkxEasyConvertOrderStatusConverter() : this(true) { }

    protected override List<KeyValuePair<OkxEasyConvertOrderStatus, string>> Mapping =>
    [
        new(OkxEasyConvertOrderStatus.Running, "running"),
        new(OkxEasyConvertOrderStatus.Filled, "filled"),
        new(OkxEasyConvertOrderStatus.Failed, "failed"),
    ];
}