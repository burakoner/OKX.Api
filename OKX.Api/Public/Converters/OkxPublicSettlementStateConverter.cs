namespace OKX.Api.Public;

internal class OkxPublicSettlementStateConverter(bool quotes) : BaseConverter<OkxPublicSettlementState>(quotes)
{
    public OkxPublicSettlementStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxPublicSettlementState, string>> Mapping =>
    [
        new(OkxPublicSettlementState.Processing, "processing"),
        new(OkxPublicSettlementState.Settled, "settled"),
    ];
}