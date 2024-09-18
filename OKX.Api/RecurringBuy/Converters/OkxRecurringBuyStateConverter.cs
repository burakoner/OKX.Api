using OKX.Api.RecurringBuy.Enums;

namespace OKX.Api.RecurringBuy.Converters;

internal class OkxRecurringBuyStateConverter(bool quotes) : BaseConverter<OkxRecurringBuyState>(quotes)
{
    public OkxRecurringBuyStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxRecurringBuyState, string>> Mapping =>
    [
        new(OkxRecurringBuyState.Canceled, "canceled"),
        new(OkxRecurringBuyState.Live, "live"),
        new(OkxRecurringBuyState.PartiallyFilled, "partially_filled"),
        new(OkxRecurringBuyState.Filled, "filled"),
        new(OkxRecurringBuyState.Cancelling, "cancelling"),
    ];
}