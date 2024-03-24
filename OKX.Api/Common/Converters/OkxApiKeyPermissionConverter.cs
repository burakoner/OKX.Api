namespace OKX.Api.Common.Converters;

internal class OkxApiKeyPermissionConverter : BaseConverter<OkxApiKeyPermission>
{
    public OkxApiKeyPermissionConverter() : this(true) { }
    public OkxApiKeyPermissionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxApiKeyPermission, string>> Mapping =>
    [
        new(OkxApiKeyPermission.ReadOnly, "read_only"),
        new(OkxApiKeyPermission.Trade, "trade"),
        new(OkxApiKeyPermission.Withdraw, "withdraw"),
    ];
}