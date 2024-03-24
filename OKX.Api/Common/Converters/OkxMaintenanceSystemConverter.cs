namespace OKX.Api.Common.Converters;

internal class OkxMaintenanceSystemConverter : BaseConverter<OkxMaintenanceSystem>
{
    public OkxMaintenanceSystemConverter() : this(true) { }
    public OkxMaintenanceSystemConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxMaintenanceSystem, string>> Mapping =>
    [
        new(OkxMaintenanceSystem.Classic, "classic"),
        new(OkxMaintenanceSystem.Unified, "unified"),
    ];
}