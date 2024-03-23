using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxInstrumentStateConverter : BaseConverter<OkxInstrumentState>
{
    public OkxInstrumentStateConverter() : this(true) { }
    public OkxInstrumentStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxInstrumentState, string>> Mapping =>
    [
        new(OkxInstrumentState.Live, "live"),
        new(OkxInstrumentState.Suspend, "suspend"),
        new(OkxInstrumentState.PreOpen, "preopen"),
    ];
}