using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

public class OkxAccountRoleTypeConverter(bool quotes) : BaseConverter<OkxAccountRoleType>(quotes)
{
    public OkxAccountRoleTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccountRoleType, string>> Mapping =>
    [
        new(OkxAccountRoleType.GeneralUser, "0"),
        new(OkxAccountRoleType.LeadingTrader, "1"),
        new(OkxAccountRoleType.CopyTrader, "2"),
    ];
}