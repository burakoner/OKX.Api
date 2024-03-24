namespace OKX.Api.Common.Converters;

internal class OkxSelfTradePreventionModeConverter : BaseConverter<OkxSelfTradePreventionMode>
{
    public OkxSelfTradePreventionModeConverter() : this(true) { }
    public OkxSelfTradePreventionModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxSelfTradePreventionMode, string>> Mapping =>
    [
        new(OkxSelfTradePreventionMode.CancelMaker, "cancel_maker"),
        new(OkxSelfTradePreventionMode.CancelTaker, "cancel_taker"),
        new(OkxSelfTradePreventionMode.CancelBoth, "cancel_both"),
    ];
}