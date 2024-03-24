namespace OKX.Api.Common.Converters;

internal class OkxOrderBookTypeConverter : BaseConverter<OkxOrderBookType>
{
    public OkxOrderBookTypeConverter() : this(true) { }
    public OkxOrderBookTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxOrderBookType, string>> Mapping =>
    [
        new(OkxOrderBookType.OrderBook, "books"),
        new(OkxOrderBookType.OrderBook_5, "books5"),
        new(OkxOrderBookType.OrderBook_50_l2_TBT, "books50-l2-tbt"),
        new(OkxOrderBookType.OrderBook_l2_TBT, "books-l2-tbt"),
        new(OkxOrderBookType.BBO_TBT, "bbo-tbt"),
    ];
}