using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxOptionsPriceTypeConverter(bool quotes) : BaseConverter<OkxOptionsPriceType>(quotes)
{
    public OkxOptionsPriceTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxOptionsPriceType, string>> Mapping =>
    [
        new(OkxOptionsPriceType.Price, "px"),
        new(OkxOptionsPriceType.PriceUsd, "pxUsd"),
        new(OkxOptionsPriceType.PriceVolatility, "pxVol"),
    ];
}