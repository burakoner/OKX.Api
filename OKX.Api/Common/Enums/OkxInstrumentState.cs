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
    Live,

    /// <summary>
    /// Suspend
    /// </summary>
    [Map("suspend")]
    Suspend,

    /// <summary>
    /// PreOpen
    /// </summary>
    [Map("preopen")]
    PreOpen,
}