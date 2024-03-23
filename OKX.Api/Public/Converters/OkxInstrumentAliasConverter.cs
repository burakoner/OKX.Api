using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxInstrumentAliasConverter : BaseConverter<OkxInstrumentAlias>
{
    public OkxInstrumentAliasConverter() : this(true) { }
    public OkxInstrumentAliasConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxInstrumentAlias, string>> Mapping =>
    [
        new(OkxInstrumentAlias.ThisWeek, "this_week"),
        new(OkxInstrumentAlias.NextWeek, "next_week"),
        new(OkxInstrumentAlias.Quarter, "quarter"),
        new(OkxInstrumentAlias.NextQuarter, "next_quarter"),
    ];
}