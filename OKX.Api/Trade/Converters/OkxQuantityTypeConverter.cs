using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

public class OkxQuantityTypeConverter(bool quotes) : BaseConverter<OkxQuantityType>(quotes)
{
    public OkxQuantityTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxQuantityType, string>> Mapping =>
    [
        new(OkxQuantityType.BaseCurrency, "base_ccy"),
        new(OkxQuantityType.QuoteCurrency, "quote_ccy"),
    ];
}