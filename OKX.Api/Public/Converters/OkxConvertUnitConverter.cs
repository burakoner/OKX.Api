using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxConvertUnitConverter : BaseConverter<OkxConvertUnit>
{
    public OkxConvertUnitConverter() : this(true) { }
    public OkxConvertUnitConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxConvertUnit, string>> Mapping =>
    [
        new(OkxConvertUnit.Coin, "coin"),
        new(OkxConvertUnit.Usds, "usds"),
    ];
}