namespace OKX.Api.Public;

internal class OkxPublicConvertUnitConverter(bool quotes) : BaseConverter<OkxPublicConvertUnit>(quotes)
{
    public OkxPublicConvertUnitConverter() : this(true) { }

    protected override List<KeyValuePair<OkxPublicConvertUnit, string>> Mapping =>
    [
        new(OkxPublicConvertUnit.Coin, "coin"),
        new(OkxPublicConvertUnit.Usds, "usds"),
    ];
}