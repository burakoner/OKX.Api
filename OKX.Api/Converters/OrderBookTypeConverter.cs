namespace OKX.Api.Converters;

internal class OrderBookTypeConverter : BaseConverter<OkxOrderBookType>
{
    public OrderBookTypeConverter() : this(true) { }
    public OrderBookTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxOrderBookType, string>> Mapping => new List<KeyValuePair<OkxOrderBookType, string>>
    {
        new KeyValuePair<OkxOrderBookType, string>(OkxOrderBookType.OrderBook, "books"),
        new KeyValuePair<OkxOrderBookType, string>(OkxOrderBookType.OrderBook_5, "books5"),
        new KeyValuePair<OkxOrderBookType, string>(OkxOrderBookType.OrderBook_50_l2_TBT, "books50-l2-tbt"),
        new KeyValuePair<OkxOrderBookType, string>(OkxOrderBookType.OrderBook_l2_TBT, "books-l2-tbt"),
        new KeyValuePair<OkxOrderBookType, string>(OkxOrderBookType.BBO_TBT, "bbo-tbt"),
    };
}