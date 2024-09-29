namespace OKX.Api.Block;

internal class OkxBlockStateConverter(bool quotes) : BaseConverter<OkxBlockState>(quotes)
{
    public OkxBlockStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxBlockState, string>> Mapping =>
    [
        new(OkxBlockState.Active, "active"),
        new(OkxBlockState.Canceled, "canceled"),
        new(OkxBlockState.PendingFill, "pending_fill"),
        new(OkxBlockState.TradedAway, "traded_away"),
        new(OkxBlockState.Filled, "filled"),
        new(OkxBlockState.Expired, "expired"),
        new(OkxBlockState.Failed, "failed"),
    ];
}