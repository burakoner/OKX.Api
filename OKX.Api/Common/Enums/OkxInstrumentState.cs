namespace OKX.Api.Common;

/// <summary>
/// OKX Instrument State
/// </summary>
public enum OkxInstrumentState : byte
{
    /// <summary>
    /// Live
    /// </summary>
    [Map("live")]
    Live = 1,

    /// <summary>
    /// Suspend
    /// </summary>
    [Map("suspend")]
    Suspend = 2,

    /// <summary>
    /// Rebase
    /// Can't be traded during rebasing. Only applicable to SWAP.
    /// </summary>
    [Map("rebase")]
    Rebase = 3,

    /// <summary>
    /// PreOpen
    /// </summary>
    [Map("preopen")]
    PreOpen = 4,
}
