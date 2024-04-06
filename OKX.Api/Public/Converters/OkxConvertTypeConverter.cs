using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

public class OkxConvertTypeConverter(bool quotes) : BaseConverter<OkxConvertType>(quotes)
{
    public OkxConvertTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxConvertType, string>> Mapping =>
    [
        new(OkxConvertType.CurrencyToContract, "1"),
        new(OkxConvertType.ContractToCurrency, "2"),
    ];
}