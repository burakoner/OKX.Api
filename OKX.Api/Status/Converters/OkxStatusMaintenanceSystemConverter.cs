using OKX.Api.Status.Enums;

namespace OKX.Api.Status.Converters;

internal class OkxStatusMaintenanceSystemConverter(bool quotes) : BaseConverter<OkxStatusMaintenanceSystem>(quotes)
{
    public OkxStatusMaintenanceSystemConverter() : this(true) { }

    protected override List<KeyValuePair<OkxStatusMaintenanceSystem, string>> Mapping =>
    [
        new(OkxStatusMaintenanceSystem.Classic, "classic"),
        new(OkxStatusMaintenanceSystem.Unified, "unified"),
    ];
}