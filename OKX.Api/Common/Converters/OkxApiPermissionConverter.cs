namespace OKX.Api.Common.Converters;

internal class OkxApiPermissionConverter : BaseConverter<OkxApiPermission>
{
    public OkxApiPermissionConverter() : this(true) { }
    public OkxApiPermissionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxApiPermission, string>> Mapping =>
    [
        new(OkxApiPermission.ReadOnly, "read_only"),
        new(OkxApiPermission.Trade, "trade"),
    ];
}