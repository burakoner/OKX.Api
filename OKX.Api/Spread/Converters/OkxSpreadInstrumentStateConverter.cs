using OKX.Api.Spread.Enums;

namespace OKX.Api.Spread.Converters;

internal class OkxSpreadInstrumentStateConverter(bool quotes) : BaseConverter<OkxSpreadInstrumentState>(quotes)
{
    public OkxSpreadInstrumentStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSpreadInstrumentState, string>> Mapping =>
    [
        new(OkxSpreadInstrumentState.Live, "live"),
        new(OkxSpreadInstrumentState.Suspended, "suspend"),
        new(OkxSpreadInstrumentState.Expired, "expired"),
    ];
}