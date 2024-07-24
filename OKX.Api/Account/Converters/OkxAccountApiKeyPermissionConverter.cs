using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxAccountApiKeyPermissionConverter(bool quotes) : BaseConverter<OkxAccountApiKeyPermission>(quotes)
{
    public OkxAccountApiKeyPermissionConverter() : this(true) { }

    protected override List<KeyValuePair<OkxAccountApiKeyPermission, string>> Mapping =>
    [
        new(OkxAccountApiKeyPermission.ReadOnly, "read_only"),
        new(OkxAccountApiKeyPermission.Trade, "trade"),
        new(OkxAccountApiKeyPermission.Withdraw, "withdraw"),
    ];
}