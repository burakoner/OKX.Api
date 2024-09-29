namespace OKX.Api.Public;

internal class OkxPublicEventImportanceConverter(bool quotes) : BaseConverter<OkxPublicEventImportance>(quotes)
{
    public OkxPublicEventImportanceConverter() : this(true) { }

    protected override List<KeyValuePair<OkxPublicEventImportance, string>> Mapping =>
    [
        new(OkxPublicEventImportance.Low, "1"),
        new(OkxPublicEventImportance.Medium, "2"),
        new(OkxPublicEventImportance.High, "3"),
    ];
}