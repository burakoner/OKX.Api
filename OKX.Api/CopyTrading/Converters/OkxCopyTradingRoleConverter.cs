using OKX.Api.CopyTrading.Enums;

namespace OKX.Api.CopyTrading.Converters;

public class OkxCopyTradingRoleConverter(bool quotes) : BaseConverter<OkxCopyTradingRole>(quotes)
{
    public OkxCopyTradingRoleConverter() : this(true) { }

    protected override List<KeyValuePair<OkxCopyTradingRole, string>> Mapping =>
    [
        new(OkxCopyTradingRole.GeneralUser, "0"),
        new(OkxCopyTradingRole.LeadingTrader, "1"),
        new(OkxCopyTradingRole.CopyTrader, "2"),
    ];
}