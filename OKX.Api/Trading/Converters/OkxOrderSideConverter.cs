using OKX.Api.Trading.Enums;

namespace OKX.Api.Trading.Converters;

internal class OkxOrderSideConverter(bool quotes) : BaseConverter<OkxOrderSide>(quotes)
{
    public OkxOrderSideConverter() : this(true) { }

    protected override List<KeyValuePair<OkxOrderSide, string>> Mapping =>
    [
        new(OkxOrderSide.Buy, "buy"),
        new(OkxOrderSide.Sell, "sell"),
    ];
}