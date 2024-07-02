using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxEventImportanceConverter(bool quotes) : BaseConverter<OkxEventImportance>(quotes)
{
    public OkxEventImportanceConverter() : this(true) { }

    protected override List<KeyValuePair<OkxEventImportance, string>> Mapping =>
    [
        new(OkxEventImportance.Low, "1"),
        new(OkxEventImportance.Medium, "2"),
        new(OkxEventImportance.High, "3"),
    ];
}