namespace OKX.Api.Common.Converters;

internal class OkxMaintenanceSystemConverter(bool quotes) : BaseConverter<OkxMaintenanceSystem>(quotes)
{
    public OkxMaintenanceSystemConverter() : this(true) { }

    protected override List<KeyValuePair<OkxMaintenanceSystem, string>> Mapping =>
    [
        new(OkxMaintenanceSystem.Classic, "classic"),
        new(OkxMaintenanceSystem.Unified, "unified"),
    ];
}