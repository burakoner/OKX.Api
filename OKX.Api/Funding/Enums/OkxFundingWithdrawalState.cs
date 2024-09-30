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
    Canceling = -3,

    /// <summary>
    /// Canceled
    /// </summary>
    [Map("-2")]
    Canceled = -2,

    /// <summary>
    /// Failed
    /// </summary>
    [Map("-1")]
    Failed = -1,

    /// <summary>
    /// Pending
    /// </summary>
    [Map("0")]
    Pending = 0,

    /// <summary>
    /// Withdrawing
    /// </summary>
    [Map("1")]
    Withdrawing = 1,

    /// <summary>
    /// Withdraw Success
    /// </summary>
    [Map("2")]
    WithdrawSuccess = 2,

    /// <summary>
    /// Approved
    /// </summary>
    [Map("7")]
    Approved = 7,

    /// <summary>
    /// Waiting Transfer
    /// </summary>
    [Map("10")]
    WaitingTransfer = 10,

    /// <summary>
    /// Waiting Manual Review
    /// </summary>
    [Map("4", "5", "6", "8", "9", "12")]
    WaitingManualReview = 4,

    /// <summary>
    /// PendingTransactionValidation
    /// </summary>
    [Map("15")]
    PendingTransactionValidation = 15,

    /// <summary>
    /// WithdrawalMayTakeUpTo24Hours
    /// </summary>
    [Map("16")]
    WithdrawalMayTakeUpTo24Hours = 16
}