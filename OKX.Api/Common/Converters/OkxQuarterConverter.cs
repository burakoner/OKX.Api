using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

internal class OkxQuarterConverter(bool quotes) : BaseConverter<OkxQuarter>(quotes)
{
    public OkxQuarterConverter() : this(true) { }

    protected override List<KeyValuePair<OkxQuarter, string>> Mapping =>
    [
        new(OkxQuarter.Quarter1, "Q1"),
        new(OkxQuarter.Quarter2, "Q2"),
        new(OkxQuarter.Quarter3, "Q3"),
        new(OkxQuarter.Quarter4, "Q4"),
    ];
}