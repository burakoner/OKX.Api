namespace OKX.Api.Account;

/// <summary>
/// OKX Account Move Position State
/// </summary>
public enum OkxAccountMovePositionState : byte
{
    /// <summary>
    /// Pending
    /// </summary>
    [Map("pending")]
    Pending = 1,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled = 2,

    /// <summary>
    /// Failed
    /// </summary>
    [Map("failed")]
    Failed = 3,
}