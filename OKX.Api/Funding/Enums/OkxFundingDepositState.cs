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
    WaitingForConfirmation = 1,

    /// <summary>
    /// Credited
    /// </summary>
    [Map("2")]
    Credited = 2,

    /// <summary>
    /// Successful
    /// </summary>
    [Map("3")]
    Successful = 3,

    /// <summary>
    /// Pending
    /// </summary>
    [Map("4")]
    Pending = 4,
}