using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

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
        new(OkxFundingWithdrawalState.WaitingManualReview04, "4"),
        new(OkxFundingWithdrawalState.WaitingManualReview05, "5"),
        new(OkxFundingWithdrawalState.WaitingManualReview06, "6"),
        new(OkxFundingWithdrawalState.WaitingManualReview08, "8"),
        new(OkxFundingWithdrawalState.WaitingManualReview09, "9"),
        new(OkxFundingWithdrawalState.WaitingManualReview12, "12"),
    ];
}