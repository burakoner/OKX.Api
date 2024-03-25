using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

internal class OkxApiKeyPermissionConverter(bool quotes) : BaseConverter<OkxApiKeyPermission>(quotes)
{
    public OkxApiKeyPermissionConverter() : this(true) { }

    protected override List<KeyValuePair<OkxApiKeyPermission, string>> Mapping =>
    [
        new(OkxApiKeyPermission.ReadOnly, "read_only"),
        new(OkxApiKeyPermission.Trade, "trade"),
        new(OkxApiKeyPermission.Withdraw, "withdraw"),
    ];
}