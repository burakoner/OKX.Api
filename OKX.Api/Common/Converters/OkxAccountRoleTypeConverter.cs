namespace OKX.Api.Common.Converters;

internal class OkxAccountRoleTypeConverter : BaseConverter<OkxAccountRoleType>
{
    public OkxAccountRoleTypeConverter() : this(true) { }
    public OkxAccountRoleTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAccountRoleType, string>> Mapping =>
    [
        new(OkxAccountRoleType.GeneralUser, "0"),
        new(OkxAccountRoleType.LeadingTrader, "1"),
        new(OkxAccountRoleType.CopyTrader, "2"),
    ];
}