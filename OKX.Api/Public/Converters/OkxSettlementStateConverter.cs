using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxSettlementStateConverter(bool quotes) : BaseConverter<OkxSettlementState>(quotes)
{
    public OkxSettlementStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxSettlementState, string>> Mapping =>
    [
        new(OkxSettlementState.Processing, "processing"),
        new(OkxSettlementState.Settled, "settled"),
    ];
}