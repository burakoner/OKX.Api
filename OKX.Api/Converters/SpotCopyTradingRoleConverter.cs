namespace OKX.Api.Converters;

internal class SpotCopyTradingRoleConverter : BaseConverter<OkxSpotCopyTradingRole>
{
    public SpotCopyTradingRoleConverter() : this(true) { }
    public SpotCopyTradingRoleConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxSpotCopyTradingRole, string>> Mapping => new()
    {
        new KeyValuePair<OkxSpotCopyTradingRole, string>(OkxSpotCopyTradingRole.GeneralUser, "0"),
        new KeyValuePair<OkxSpotCopyTradingRole, string>(OkxSpotCopyTradingRole.LeadingTrader, "1"),
        new KeyValuePair<OkxSpotCopyTradingRole, string>(OkxSpotCopyTradingRole.CopyTrader, "2"),
    };
}