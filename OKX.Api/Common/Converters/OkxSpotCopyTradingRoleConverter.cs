namespace OKX.Api.Common.Converters;

internal class OkxSpotCopyTradingRoleConverter : BaseConverter<OkxSpotCopyTradingRole>
{
    public OkxSpotCopyTradingRoleConverter() : this(true) { }
    public OkxSpotCopyTradingRoleConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxSpotCopyTradingRole, string>> Mapping =>
    [
        new(OkxSpotCopyTradingRole.GeneralUser, "0"),
        new(OkxSpotCopyTradingRole.LeadingTrader, "1"),
        new(OkxSpotCopyTradingRole.CopyTrader, "2"),
    ];
}