using OKX.Api.Trading.Enums;

namespace OKX.Api.Trading.Converters;

internal class OkxOneClickRepayOrderStatusConverter(bool quotes) : BaseConverter<OkxOneClickRepayOrderStatus>(quotes)
{
    public OkxOneClickRepayOrderStatusConverter() : this(true) { }

    protected override List<KeyValuePair<OkxOneClickRepayOrderStatus, string>> Mapping =>
    [
        new(OkxOneClickRepayOrderStatus.Running, "running"),
        new(OkxOneClickRepayOrderStatus.Filled, "filled"),
        new(OkxOneClickRepayOrderStatus.Failed, "failed"),
    ];
}