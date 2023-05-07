namespace OKX.Api.Converters;

internal class WithdrawalStateConverter : BaseConverter<OkxWithdrawalState>
{
    public WithdrawalStateConverter() : this(true) { }
    public WithdrawalStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxWithdrawalState, string>> Mapping => new List<KeyValuePair<OkxWithdrawalState, string>>
    {
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.Canceling, "-3"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.Canceled, "-2"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.Failed, "-1"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.Pending, "0"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.Withdrawing, "1"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.WithdrawSuccess, "2"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.Approved, "7"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.WaitingTransfer, "10"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.WaitingManualReview04, "4"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.WaitingManualReview05, "5"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.WaitingManualReview06, "6"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.WaitingManualReview08, "8"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.WaitingManualReview09, "9"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.WaitingManualReview12, "12"),
    };
}