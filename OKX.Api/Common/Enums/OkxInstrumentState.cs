namespace OKX.Api.Common;

/// <summary>
/// OKX Instrument State
/// </summary>
public enum OkxInstrumentState
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
    /// PreOpen
    /// </summary>
    [Map("preopen")]
    PreOpen = 3,
}