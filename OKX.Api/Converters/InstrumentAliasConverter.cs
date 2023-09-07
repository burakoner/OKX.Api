namespace OKX.Api.Converters;

internal class InstrumentAliasConverter : BaseConverter<OkxInstrumentAlias>
{
    public InstrumentAliasConverter() : this(true) { }
    public InstrumentAliasConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxInstrumentAlias, string>> Mapping => new List<KeyValuePair<OkxInstrumentAlias, string>>
    {
        new KeyValuePair<OkxInstrumentAlias, string>(OkxInstrumentAlias.ThisWeek, "this_week"),
        new KeyValuePair<OkxInstrumentAlias, string>(OkxInstrumentAlias.NextWeek, "next_week"),
        new KeyValuePair<OkxInstrumentAlias, string>(OkxInstrumentAlias.Quarter, "quarter"),
        new KeyValuePair<OkxInstrumentAlias, string>(OkxInstrumentAlias.NextQuarter, "next_quarter"),
    };
}