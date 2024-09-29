namespace OKX.Api.Funding;

internal class OkxFundingTransferStateConverter(bool quotes) : BaseConverter<OkxFundingTransferState>(quotes)
{
    public OkxFundingTransferStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFundingTransferState, string>> Mapping =>
    [
        new(OkxFundingTransferState.Success, "success"),
        new(OkxFundingTransferState.Pending, "pending"),
        new(OkxFundingTransferState.Failed, "failed"),
    ];
}