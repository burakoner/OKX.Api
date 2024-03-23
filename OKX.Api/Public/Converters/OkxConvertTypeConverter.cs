using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxConvertTypeConverter : BaseConverter<OkxConvertType>
{
    public OkxConvertTypeConverter() : this(true) { }
    public OkxConvertTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxConvertType, string>> Mapping =>
    [
        new(OkxConvertType.CurrencyToContract, "1"),
        new(OkxConvertType.ContractToCurrency, "2"),
    ];
}