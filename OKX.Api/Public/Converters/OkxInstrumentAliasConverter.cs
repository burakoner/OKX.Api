using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxInstrumentAliasConverter(bool quotes) : BaseConverter<OkxInstrumentAlias>(quotes)
{
    public OkxInstrumentAliasConverter() : this(true) { }

    protected override List<KeyValuePair<OkxInstrumentAlias, string>> Mapping =>
    [
        new(OkxInstrumentAlias.ThisWeek, "this_week"),
        new(OkxInstrumentAlias.NextWeek, "next_week"),
        new(OkxInstrumentAlias.Quarter, "quarter"),
        new(OkxInstrumentAlias.NextQuarter, "next_quarter"),
    ];
}