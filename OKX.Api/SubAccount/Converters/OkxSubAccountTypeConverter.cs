using OKX.Api.SubAccount.Enums;

namespace OKX.Api.SubAccount.Converters;

internal class OkxSubAccountTypeConverter(bool quotes) : BaseConverter<OkxSubAccountType>(quotes)
{
    public OkxSubAccountTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSubAccountType, string>> Mapping =>
    [
        new(OkxSubAccountType.Standard, "1"),
        new(OkxSubAccountType.Custody, "2"),
    ];
}