using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

public class OkxConvertUnitConverter(bool quotes) : BaseConverter<OkxConvertUnit>(quotes)
{
    public OkxConvertUnitConverter() : this(true) { }

    protected override List<KeyValuePair<OkxConvertUnit, string>> Mapping =>
    [
        new(OkxConvertUnit.Coin, "coin"),
        new(OkxConvertUnit.Usds, "usds"),
    ];
}