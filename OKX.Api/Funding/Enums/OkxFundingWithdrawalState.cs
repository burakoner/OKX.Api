namespace OKX.Api.Funding;

/// <summary>
/// OKX Withdrawal State
/// </summary>
public enum OkxFundingWithdrawalState
{
    /// <summary>
    /// Canceling
    /// </summary>
    [Map("-3")]
    Canceling,

    /// <summary>
    /// Canceled
    /// </summary>
    [Map("-2")]
    Canceled,

    /// <summary>
    /// Failed
    /// </summary>
    [Map("-1")]
    Failed,

    /// <summary>
    /// Pending
    /// </summary>
    [Map("0")]
    Pending,

    /// <summary>
    /// Withdrawing
    /// </summary>
    [Map("1")]
    Withdrawing,

    /// <summary>
    /// Withdraw Success
    /// </summary>
    [Map("2")]
    WithdrawSuccess,

    /// <summary>
    /// Approved
    /// </summary>
    [Map("7")]
    Approved,

    /// <summary>
    /// Waiting Transfer
    /// </summary>
    [Map("10")]
    WaitingTransfer,

    /// <summary>
    /// Waiting Manual Review
    /// </summary>
    [Map("4", "5", "6", "8", "9", "12")]
    WaitingManualReview,

    /// <summary>
    /// PendingTransactionValidation
    /// </summary>
    [Map("15")]
    PendingTransactionValidation,

    /// <summary>
    /// WithdrawalMayTakeUpTo24Hours
    /// </summary>
    [Map("16")]
    WithdrawalMayTakeUpTo24Hours
}