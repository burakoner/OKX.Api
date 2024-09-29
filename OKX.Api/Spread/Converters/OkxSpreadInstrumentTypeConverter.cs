namespace OKX.Api.Spread;

internal class OkxSpreadInstrumentTypeConverter(bool quotes) : BaseConverter<OkxSpreadInstrumentType>(quotes)
{
    public OkxSpreadInstrumentTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSpreadInstrumentType, string>> Mapping =>
    [
        new(OkxSpreadInstrumentType.Linear, "linear"),
        new(OkxSpreadInstrumentType.Inverse, "inverse"),
        new(OkxSpreadInstrumentType.Hybrid, "hybrid"),
    ];
}