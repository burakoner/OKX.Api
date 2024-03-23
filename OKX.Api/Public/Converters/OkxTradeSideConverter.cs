namespace OKX.Api.Public.Converters;

internal class OkxTradeSideConverter : BaseConverter<OkxTradeSide>
{
    public OkxTradeSideConverter() : this(true) { }
    public OkxTradeSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxTradeSide, string>> Mapping =>
    [
        new(OkxTradeSide.Buy, "buy"),
        new(OkxTradeSide.Sell, "sell"),
    ];
}