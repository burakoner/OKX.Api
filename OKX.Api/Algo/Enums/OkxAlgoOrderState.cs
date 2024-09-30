namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Order State
/// </summary>
public enum OkxAlgoOrderState
{
    /// <summary>
    /// Live
    /// </summary>
    [Map("live")]
    Live,

    /// <summary>
    /// Pause
    /// </summary>
    [Map("pause")]
    Pause,

    /// <summary>
    /// Effective
    /// </summary>
    [Map("effective")]
    Effective,

    /// <summary>
    /// PartiallyEffective
    /// </summary>
    [Map("partially_effective")]
    PartiallyEffective,

    /// <summary>
    /// PartiallyFailed
    /// </summary>
    [Map("partially_failed")]
    PartiallyFailed,

    /// <summary>
    /// Canceled
    /// </summary>
    [Map("canceled")]
    Canceled,

    /// <summary>
    /// Failed
    /// </summary>
    [Map("order_failed")]
    Failed,
}