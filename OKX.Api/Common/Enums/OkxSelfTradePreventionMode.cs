namespace OKX.Api.Common;

/// <summary>
/// OKX Self Trade Prevention Mode
/// </summary>
public enum OkxSelfTradePreventionMode
{
    /// <summary>
    /// CancelMaker
    /// </summary>
    [Map("cancel_maker")]
    CancelMaker,

    /// <summary>
    /// CancelTaker
    /// </summary>
    [Map("cancel_taker")]
    CancelTaker,

    /// <summary>
    /// CancelBoth
    /// </summary>
    [Map("cancel_both")]
    CancelBoth
}