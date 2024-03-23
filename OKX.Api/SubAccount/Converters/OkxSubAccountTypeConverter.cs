using OKX.Api.SubAccount.Enums;

namespace OKX.Api.SubAccount.Converters;

internal class OkxSubAccountTypeConverter : BaseConverter<OkxSubAccountType>
{
    public OkxSubAccountTypeConverter() : this(true) { }
    public OkxSubAccountTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxSubAccountType, string>> Mapping =>
    [
        new(OkxSubAccountType.Standard, "1"),
        new(OkxSubAccountType.Custody, "2"),
    ];
}