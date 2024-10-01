namespace OKX.Api.CopyTrading;
/// <summary>
/// Okx Multiple Operation Status
/// </summary>
public enum OkxCopyTradingOperationStatus
{
    /// <summary>
    /// All success
    /// </summary>
    [Map("0")]
    AllSuccess = 1,

    /// <summary>
    /// Some successes
    /// </summary>
    [Map("1")]
    SomeSuccesses = 1,

    /// <summary>
    /// All fail
    /// </summary>
    [Map("2")]
    AllFail = 2,
}