namespace OKX.Api.Funding;

/// <summary>
/// OKX Withdrawal State
/// </summary>
public enum OkxFundingWithdrawalState
{
    /// <summary>
    /// Canceling
    /// </summary>
    Canceling,

    /// <summary>
    /// Canceled
    /// </summary>
    Canceled,

    /// <summary>
    /// Failed
    /// </summary>
    Failed,

    /// <summary>
    /// Pending
    /// </summary>
    Pending,

    /// <summary>
    /// Withdrawing
    /// </summary>
    Withdrawing,

    /// <summary>
    /// WithdrawSuccess
    /// </summary>
    WithdrawSuccess,

    /// <summary>
    /// Approved
    /// </summary>
    Approved,

    /// <summary>
    /// WaitingTransfer
    /// </summary>
    WaitingTransfer,

    /// <summary>
    /// WaitingManualReview04
    /// </summary>
    WaitingManualReview04,

    /// <summary>
    /// WaitingManualReview05
    /// </summary>
    WaitingManualReview05,

    /// <summary>
    /// WaitingManualReview06
    /// </summary>
    WaitingManualReview06,

    /// <summary>
    /// WaitingManualReview08
    /// </summary>
    WaitingManualReview08,

    /// <summary>
    /// WaitingManualReview09
    /// </summary>
    WaitingManualReview09,

    /// <summary>
    /// WaitingManualReview12
    /// </summary>
    WaitingManualReview12,

    /// <summary>
    /// PendingTransactionValidation
    /// </summary>
    PendingTransactionValidation,

    /// <summary>
    /// WithdrawalMayTakeUpTo24Hours
    /// </summary>
    WithdrawalMayTakeUpTo24Hours
}