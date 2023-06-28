namespace OKX.Api.Converters;

internal class ApiKeyPermissionConverter : BaseConverter<OkxApiKeyPermission>
{
    public ApiKeyPermissionConverter() : this(true) { }
    public ApiKeyPermissionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxApiKeyPermission, string>> Mapping => new List<KeyValuePair<OkxApiKeyPermission, string>>
    {
        new KeyValuePair<OkxApiKeyPermission, string>(OkxApiKeyPermission.ReadOnly, "read_only"),
        new KeyValuePair<OkxApiKeyPermission, string>(OkxApiKeyPermission.Trade, "trade"),
        new KeyValuePair<OkxApiKeyPermission, string>(OkxApiKeyPermission.Withdraw, "withdraw"),
    };
}