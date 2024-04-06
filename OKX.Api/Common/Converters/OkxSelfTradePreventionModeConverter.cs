using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

public class OkxSelfTradePreventionModeConverter(bool quotes) : BaseConverter<OkxSelfTradePreventionMode>(quotes)
{
    public OkxSelfTradePreventionModeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSelfTradePreventionMode, string>> Mapping =>
    [
        new(OkxSelfTradePreventionMode.CancelMaker, "cancel_maker"),
        new(OkxSelfTradePreventionMode.CancelTaker, "cancel_taker"),
        new(OkxSelfTradePreventionMode.CancelBoth, "cancel_both"),
    ];
}