using OKX.Api.RecurringBuy.Enums;

namespace OKX.Api.RecurringBuy.Converters;

internal class OkxRecurringBuyPeriodConverter(bool quotes) : BaseConverter<OkxRecurringBuyPeriod>(quotes)
{
    public OkxRecurringBuyPeriodConverter() : this(true) { }

    protected override List<KeyValuePair<OkxRecurringBuyPeriod, string>> Mapping =>
    [
        new(OkxRecurringBuyPeriod.Monthly, "monthly"),
        new(OkxRecurringBuyPeriod.Weekly, "weekly"),
        new(OkxRecurringBuyPeriod.Daily, "daily"),
        new(OkxRecurringBuyPeriod.Hourly, "hourly"),
    ];
}