using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

internal class OkxApiPermissionConverter(bool quotes) : BaseConverter<OkxApiPermission>(quotes)
{
    public OkxApiPermissionConverter() : this(true) { }

    protected override List<KeyValuePair<OkxApiPermission, string>> Mapping =>
    [
        new(OkxApiPermission.ReadOnly, "read_only"),
        new(OkxApiPermission.Trade, "trade"),
    ];
}