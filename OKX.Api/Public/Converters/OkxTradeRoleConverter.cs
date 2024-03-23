using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxTradeRoleConverter : BaseConverter<OkxTradeRole>
{
    public OkxTradeRoleConverter() : this(true) { }
    public OkxTradeRoleConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxTradeRole, string>> Mapping =>
    [
        new(OkxTradeRole.Taker, "T"),
        new(OkxTradeRole.Maker, "M"),
    ];
}