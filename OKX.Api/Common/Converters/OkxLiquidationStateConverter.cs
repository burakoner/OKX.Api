using OKX.Api.Common.Enums;

namespace OKX.Api.Common.Converters;

public class OkxLiquidationStateConverter(bool quotes) : BaseConverter<OkxLiquidationState>(quotes)
{
    public OkxLiquidationStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxLiquidationState, string>> Mapping =>
    [
        new(OkxLiquidationState.Unfilled, "unfilled"),
        new(OkxLiquidationState.Filled, "filled"),
    ];
}