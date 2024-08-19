using OKX.Api.Spread.Enums;

namespace OKX.Api.Spread.Converters;

internal class OkxSpreadOrderTypeConverter(bool quotes) : BaseConverter<OkxSpreadOrderType>(quotes)
{
    public OkxSpreadOrderTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSpreadOrderType, string>> Mapping =>
    [
        new(OkxSpreadOrderType.LimitOrder, "limit"),
        new(OkxSpreadOrderType.PostOnly, "post_only"),
        new(OkxSpreadOrderType.ImmediateOrCancel, "ioc"),
    ];
}