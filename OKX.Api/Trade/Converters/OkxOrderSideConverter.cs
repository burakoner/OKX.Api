using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxOrderSideConverter : BaseConverter<OkxOrderSide>
{
    public OkxOrderSideConverter() : this(true) { }
    public OkxOrderSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxOrderSide, string>> Mapping => new List<KeyValuePair<OkxOrderSide, string>>
    {
        new KeyValuePair<OkxOrderSide, string>(OkxOrderSide.Buy, "buy"),
        new KeyValuePair<OkxOrderSide, string>(OkxOrderSide.Sell, "sell"),
    };
}