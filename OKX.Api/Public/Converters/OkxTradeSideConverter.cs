namespace OKX.Api.Public;

internal class OkxTradeSideConverter(bool quotes) : BaseConverter<OkxTradeSide>(quotes)
{
    public OkxTradeSideConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradeSide, string>> Mapping =>
    [
        new(OkxTradeSide.Buy, "buy"),
        new(OkxTradeSide.Sell, "sell"),
    ];
}