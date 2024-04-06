using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

public class OkxWithdrawalStateConverter(bool quotes) : BaseConverter<OkxWithdrawalState>(quotes)
{
    public OkxWithdrawalStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxWithdrawalState, string>> Mapping =>
    [
        new(OkxWithdrawalState.Canceling, "-3"),
        new(OkxWithdrawalState.Canceled, "-2"),
        new(OkxWithdrawalState.Failed, "-1"),
        new(OkxWithdrawalState.Pending, "0"),
        new(OkxWithdrawalState.Withdrawing, "1"),
        new(OkxWithdrawalState.WithdrawSuccess, "2"),
        new(OkxWithdrawalState.Approved, "7"),
        new(OkxWithdrawalState.WaitingTransfer, "10"),
        new(OkxWithdrawalState.WaitingManualReview04, "4"),
        new(OkxWithdrawalState.WaitingManualReview05, "5"),
        new(OkxWithdrawalState.WaitingManualReview06, "6"),
        new(OkxWithdrawalState.WaitingManualReview08, "8"),
        new(OkxWithdrawalState.WaitingManualReview09, "9"),
        new(OkxWithdrawalState.WaitingManualReview12, "12"),
    ];
}