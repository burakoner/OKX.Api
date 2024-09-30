namespace OKX.Api.Funding;

/// <summary>
/// OKX Deposit State
/// </summary>
public enum OkxFundingDepositState
{
    /// <summary>
    /// WaitingForConfirmation
    /// </summary>
    [Map("1")]
    WaitingForConfirmation,

    /// <summary>
    /// Credited
    /// </summary>
    [Map("2")]
    Credited,

    /// <summary>
    /// Successful
    /// </summary>
    [Map("3")]
    Successful,

    /// <summary>
    /// Pending
    /// </summary>
    [Map("4")]
    Pending,
}