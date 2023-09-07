namespace OKX.Api.Converters;

internal class SelfTradePreventionModeConverter : BaseConverter<OkxSelfTradePreventionMode>
{
    public SelfTradePreventionModeConverter() : this(true) { }
    public SelfTradePreventionModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxSelfTradePreventionMode, string>> Mapping => new List<KeyValuePair<OkxSelfTradePreventionMode, string>>
    {
        new KeyValuePair<OkxSelfTradePreventionMode, string>(OkxSelfTradePreventionMode.CancelMaker, "cancel_maker"),
        new KeyValuePair<OkxSelfTradePreventionMode, string>(OkxSelfTradePreventionMode.CancelTaker, "cancel_taker"),
        new KeyValuePair<OkxSelfTradePreventionMode, string>(OkxSelfTradePreventionMode.CancelBoth, "cancel_both"),
    };
}