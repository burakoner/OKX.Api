namespace OKX.Api.Common.Converters;

internal class OkxPriceVarianceConverter : BaseConverter<OkxPriceVariance>
{
    public OkxPriceVarianceConverter() : this(true) { }
    public OkxPriceVarianceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxPriceVariance, string>> Mapping =>
    [
        new(OkxPriceVariance.Spread, "pxSpread"),
        new(OkxPriceVariance.Variance, "pxVar"),
    ];
}