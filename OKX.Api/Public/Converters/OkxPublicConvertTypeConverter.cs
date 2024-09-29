namespace OKX.Api.Public;

internal class OkxPublicConvertTypeConverter(bool quotes) : BaseConverter<OkxPublicConvertType>(quotes)
{
    public OkxPublicConvertTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxPublicConvertType, string>> Mapping =>
    [
        new(OkxPublicConvertType.CurrencyToContract, "1"),
        new(OkxPublicConvertType.ContractToCurrency, "2"),
    ];
}