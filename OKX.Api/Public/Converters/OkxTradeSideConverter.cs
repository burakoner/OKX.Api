using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

public class OkxTradeSideConverter(bool quotes) : BaseConverter<OkxTradeSide>(quotes)
{
    public OkxTradeSideConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradeSide, string>> Mapping =>
    [
        new(OkxTradeSide.Buy, "buy"),
        new(OkxTradeSide.Sell, "sell"),
    ];
}