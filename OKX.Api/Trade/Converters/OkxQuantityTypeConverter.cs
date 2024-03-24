using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxQuantityTypeConverter : BaseConverter<OkxQuantityType>
{
    public OkxQuantityTypeConverter() : this(true) { }
    public OkxQuantityTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxQuantityType, string>> Mapping => new List<KeyValuePair<OkxQuantityType, string>>
    {
        new KeyValuePair<OkxQuantityType, string>(OkxQuantityType.BaseCurrency, "base_ccy"),
        new KeyValuePair<OkxQuantityType, string>(OkxQuantityType.QuoteCurrency, "quote_ccy"),
    };
}