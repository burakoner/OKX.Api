namespace OKX.Api.Dca;

/// <summary>
/// OKX DCA Tracking Mode
/// </summary>
public enum OkxDcaTrackingMode : byte
{
    /// <summary>
    /// Synchronous
    /// </summary>
    [Map("sync")]
    Sync = 1,

    /// <summary>
    /// Asynchronous
    /// </summary>
    [Map("async")]
    Async = 2,
}
