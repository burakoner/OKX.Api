namespace OKX.Api.Public;

internal class OkxConvertUnitConverter(bool quotes) : BaseConverter<OkxConvertUnit>(quotes)
{
    public OkxConvertUnitConverter() : this(true) { }

    protected override List<KeyValuePair<OkxConvertUnit, string>> Mapping =>
    [
        new(OkxConvertUnit.Coin, "coin"),
        new(OkxConvertUnit.Usds, "usds"),
    ];
}