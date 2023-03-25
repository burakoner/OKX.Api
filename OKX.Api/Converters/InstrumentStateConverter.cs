namespace OKX.Api.Converters;

internal class InstrumentStateConverter : BaseConverter<OkxInstrumentState>
{
    public InstrumentStateConverter() : this(true) { }
    public InstrumentStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxInstrumentState, string>> Mapping => new List<KeyValuePair<OkxInstrumentState, string>>
    {
        new KeyValuePair<OkxInstrumentState, string>(OkxInstrumentState.Live, "live"),
        new KeyValuePair<OkxInstrumentState, string>(OkxInstrumentState.Suspend, "suspend"),
        new KeyValuePair<OkxInstrumentState, string>(OkxInstrumentState.PreOpen, "preopen"),
    };
}