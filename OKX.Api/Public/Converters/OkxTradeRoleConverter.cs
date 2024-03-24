using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxTradeRoleConverter(bool quotes) : BaseConverter<OkxTradeRole>(quotes)
{
    public OkxTradeRoleConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradeRole, string>> Mapping =>
    [
        new(OkxTradeRole.Taker, "T"),
        new(OkxTradeRole.Maker, "M"),
    ];
}