using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxOrderSideConverter : BaseConverter<OkxOrderSide>
{
    public OkxOrderSideConverter() : this(true) { }
    public OkxOrderSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxOrderSide, string>> Mapping =>
    [
        new(OkxOrderSide.Buy, "buy"),
        new(OkxOrderSide.Sell, "sell"),
    ];
}