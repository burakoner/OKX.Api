using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxEventImportanceConverter : BaseConverter<OkxEventImportance>
{
    public OkxEventImportanceConverter() : this(true) { }
    public OkxEventImportanceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxEventImportance, string>> Mapping =>
    [
        new(OkxEventImportance.Low, "1"),
        new(OkxEventImportance.Medium, "2"),
        new(OkxEventImportance.High, "3"),
    ];
}