using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

public class OkxTradeModeConverter(bool quotes) : BaseConverter<OkxTradeMode>(quotes)
{
    public OkxTradeModeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradeMode, string>> Mapping =>
    [
        new(OkxTradeMode.Cash, "cash"),
        new(OkxTradeMode.Cross, "cross"),
        new(OkxTradeMode.Isolated, "isolated"),
    ];
}