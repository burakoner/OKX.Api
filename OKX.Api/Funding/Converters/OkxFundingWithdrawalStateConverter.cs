namespace OKX.Api.Funding;

internal class OkxFundingWithdrawalStateConverter(bool quotes) : BaseConverter<OkxFundingWithdrawalState>(quotes)
{
    public OkxFundingWithdrawalStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFundingWithdrawalState, string>> Mapping =>
    [
        new(OkxFundingWithdrawalState.Canceling, "-3"),
        new(OkxFundingWithdrawalState.Canceled, "-2"),
        new(OkxFundingWithdrawalState.Failed, "-1"),
        new(OkxFundingWithdrawalState.Pending, "0"),
        new(OkxFundingWithdrawalState.Withdrawing, "1"),
        new(OkxFundingWithdrawalState.WithdrawSuccess, "2"),
        new(OkxFundingWithdrawalState.Approved, "7"),
        new(OkxFundingWithdrawalState.WaitingTransfer, "10"),
        new(OkxFundingWithdrawalState.WaitingManualReview, "4"),
        new(OkxFundingWithdrawalState.PendingTransactionValidation, "15"),
        new(OkxFundingWithdrawalState.WithdrawalMayTakeUpTo24Hours, "16"),
    ];
}