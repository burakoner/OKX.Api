namespace OKX.Api.Enums;

public enum OkxWithdrawalState
{
    PendingCancel,
    Canceled,
    Failed,
    Pending,
    Sending,
    Sent,
    AwaitingEmailVerification,
    AwaitingManualVerification,
    AwaitingIdentityVerification,
}