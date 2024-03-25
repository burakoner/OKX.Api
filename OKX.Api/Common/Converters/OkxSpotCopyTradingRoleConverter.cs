using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

internal class OkxSpotCopyTradingRoleConverter(bool quotes) : BaseConverter<OkxSpotCopyTradingRole>(quotes)
{
    public OkxSpotCopyTradingRoleConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSpotCopyTradingRole, string>> Mapping =>
    [
        new(OkxSpotCopyTradingRole.GeneralUser, "0"),
        new(OkxSpotCopyTradingRole.LeadingTrader, "1"),
        new(OkxSpotCopyTradingRole.CopyTrader, "2"),
    ];
}