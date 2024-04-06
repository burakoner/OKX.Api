using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

public class OkxTransferStateConverter(bool quotes) : BaseConverter<OkxTransferState>(quotes)
{
    public OkxTransferStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxTransferState, string>> Mapping =>
    [
        new(OkxTransferState.Success, "success"),
        new(OkxTransferState.Pending, "pending"),
        new(OkxTransferState.Failed, "failed"),
    ];
}