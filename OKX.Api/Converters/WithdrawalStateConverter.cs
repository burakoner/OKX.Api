namespace OKX.Api.Converters;

internal class WithdrawalStateConverter : BaseConverter<OkxWithdrawalState>
{
    public WithdrawalStateConverter() : this(true) { }
    public WithdrawalStateConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxWithdrawalState, string>> Mapping => new List<KeyValuePair<OkxWithdrawalState, string>>
    {
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.PendingCancel, "-3"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.Canceled, "-2"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.Failed, "-1"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.Pending, "0"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.Sending, "1"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.Sent, "2"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.AwaitingEmailVerification, "3"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.AwaitingManualVerification, "4"),
        new KeyValuePair<OkxWithdrawalState, string>(OkxWithdrawalState.AwaitingIdentityVerification, "5"),
    };
}