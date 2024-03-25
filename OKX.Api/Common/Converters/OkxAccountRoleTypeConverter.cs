using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

internal class OkxAccountRoleTypeConverter(bool quotes) : BaseConverter<OkxAccountRoleType>(quotes)
{
    public OkxAccountRoleTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccountRoleType, string>> Mapping =>
    [
        new(OkxAccountRoleType.GeneralUser, "0"),
        new(OkxAccountRoleType.LeadingTrader, "1"),
        new(OkxAccountRoleType.CopyTrader, "2"),
    ];
}