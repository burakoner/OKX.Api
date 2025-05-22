namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Trade State
/// </summary>
public enum OkxSpreadTradeState : byte
{
    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled = 1,

    /// <summary>
    /// Rejected
    /// </summary>
    [Map("rejected")]
    Rejected = 2,
}