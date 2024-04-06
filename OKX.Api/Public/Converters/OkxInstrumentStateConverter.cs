using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

public class OkxInstrumentStateConverter(bool quotes) : BaseConverter<OkxInstrumentState>(quotes)
{
    public OkxInstrumentStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxInstrumentState, string>> Mapping =>
    [
        new(OkxInstrumentState.Live, "live"),
        new(OkxInstrumentState.Suspend, "suspend"),
        new(OkxInstrumentState.PreOpen, "preopen"),
    ];
}