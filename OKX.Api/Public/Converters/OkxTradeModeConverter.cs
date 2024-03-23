using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxTradeModeConverter : BaseConverter<OkxTradeMode>
{
    public OkxTradeModeConverter() : this(true) { }
    public OkxTradeModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxTradeMode, string>> Mapping =>
    [
        new(OkxTradeMode.Cash, "cash"),
        new(OkxTradeMode.Cross, "cross"),
        new(OkxTradeMode.Isolated, "isolated"),
    ];
}