namespace OKX.Api.RecurringBuy;

internal class OkxRecurringBuyTypeConverter(bool quotes) : BaseConverter<OkxRecurringBuyType>(quotes)
{
    public OkxRecurringBuyTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxRecurringBuyType, string>> Mapping =>
    [
        new(OkxRecurringBuyType.Recurring, "recurring"),
    ];
}