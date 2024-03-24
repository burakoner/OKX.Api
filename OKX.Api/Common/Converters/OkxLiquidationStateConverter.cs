namespace OKX.Api.Common.Converters;

internal class OkxLiquidationStateConverter : BaseConverter<OkxLiquidationState>
{
    public OkxLiquidationStateConverter() : this(true) { }
    public OkxLiquidationStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxLiquidationState, string>> Mapping =>
    [
        new(OkxLiquidationState.Unfilled, "unfilled"),
        new(OkxLiquidationState.Filled, "filled"),
    ];
}