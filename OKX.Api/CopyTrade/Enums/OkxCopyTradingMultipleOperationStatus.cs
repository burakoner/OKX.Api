namespace OKX.Api.CopyTrade;
/// <summary>
/// Okx Multiple Operation Status
/// </summary>
public enum OkxCopyTradingMultipleOperationStatus
{
    /// <summary>
    /// All success
    /// </summary>
    [Map("0")]
    AllSuccess,

    /// <summary>
    /// Some successes
    /// </summary>
    [Map("1")]
    SomeSuccesses,

    /// <summary>
    /// All fail
    /// </summary>
    [Map("2")]
    AllFail,
}