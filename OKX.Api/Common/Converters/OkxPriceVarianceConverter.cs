using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

internal class OkxPriceVarianceConverter(bool quotes) : BaseConverter<OkxPriceVariance>(quotes)
{
    public OkxPriceVarianceConverter() : this(true) { }

    protected override List<KeyValuePair<OkxPriceVariance, string>> Mapping =>
    [
        new(OkxPriceVariance.Spread, "pxSpread"),
        new(OkxPriceVariance.Variance, "pxVar"),
    ];
}