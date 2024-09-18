using OKX.Api.RecurringBuy.Enums;

namespace OKX.Api.RecurringBuy.Converters;

internal class OkxRecurringBuyTypeConverter(bool quotes) : BaseConverter<OkxRecurringBuyType>(quotes)
{
    public OkxRecurringBuyTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxRecurringBuyType, string>> Mapping =>
    [
        new(OkxRecurringBuyType.Recurring, "recurring"),
    ];
}