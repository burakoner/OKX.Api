using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxTransferStateConverter : BaseConverter<OkxTransferState>
{
    public OkxTransferStateConverter() : this(true) { }
    public OkxTransferStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxTransferState, string>> Mapping =>
    [
        new(OkxTransferState.Success, "success"),
        new(OkxTransferState.Pending, "pending"),
        new(OkxTransferState.Failed, "failed"),
    ];
}