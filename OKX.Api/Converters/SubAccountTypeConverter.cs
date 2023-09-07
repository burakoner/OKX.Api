namespace OKX.Api.Converters;

internal class SubAccountTypeConverter : BaseConverter<OkxSubAccountType>
{
    public SubAccountTypeConverter() : this(true) { }
    public SubAccountTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxSubAccountType, string>> Mapping => new List<KeyValuePair<OkxSubAccountType, string>>
    {
        new KeyValuePair<OkxSubAccountType, string>(OkxSubAccountType.Standard, "1"),
        new KeyValuePair<OkxSubAccountType, string>(OkxSubAccountType.Custody, "2"),
    };
}