using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxOrderTypeConverter(bool quotes) : BaseConverter<OkxOrderType>(quotes)
{
    public OkxOrderTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxOrderType, string>> Mapping =>
    [
        new(OkxOrderType.MarketOrder, "market"),
        new(OkxOrderType.LimitOrder, "limit"),
        new(OkxOrderType.PostOnly, "post_only"),
        new(OkxOrderType.FillOrKill, "fok"),
        new(OkxOrderType.ImmediateOrCancel, "ioc"),
        new(OkxOrderType.OptimalLimitOrder, "optimal_limit_ioc"),
        new(OkxOrderType.MarketMakerProtection, "mmp"),
        new(OkxOrderType.MarektMakerProtectionAndPostOnly, "mmp_and_post_only"),
    ];
}