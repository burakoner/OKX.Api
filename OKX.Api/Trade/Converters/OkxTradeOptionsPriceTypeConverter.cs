using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxTradeOptionsPriceTypeConverter(bool quotes) : BaseConverter<OkxTradeOptionsPriceType>(quotes)
{
    public OkxTradeOptionsPriceTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTradeOptionsPriceType, string>> Mapping =>
    [
        new(OkxTradeOptionsPriceType.Price, "px"),
        new(OkxTradeOptionsPriceType.PriceUsd, "pxUsd"),
        new(OkxTradeOptionsPriceType.PriceVolatility, "pxVol"),
    ];
}