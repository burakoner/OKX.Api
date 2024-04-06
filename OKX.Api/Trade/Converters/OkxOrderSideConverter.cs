using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

public class OkxOrderSideConverter(bool quotes) : BaseConverter<OkxOrderSide>(quotes)
{
    public OkxOrderSideConverter() : this(true) { }

    protected override List<KeyValuePair<OkxOrderSide, string>> Mapping =>
    [
        new(OkxOrderSide.Buy, "buy"),
        new(OkxOrderSide.Sell, "sell"),
    ];
}