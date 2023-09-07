namespace OKX.Api.Converters;

internal class AccountRoleTypeConverter : BaseConverter<OkxAccountRoleType>
{
    public AccountRoleTypeConverter() : this(true) { }
    public AccountRoleTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxAccountRoleType, string>> Mapping => new List<KeyValuePair<OkxAccountRoleType, string>>
    {
        new KeyValuePair<OkxAccountRoleType, string>(OkxAccountRoleType.GeneralUser, "0"),
        new KeyValuePair<OkxAccountRoleType, string>(OkxAccountRoleType.LeadingTrader, "1"),
        new KeyValuePair<OkxAccountRoleType, string>(OkxAccountRoleType.CopyTrader, "2"),
    };
}