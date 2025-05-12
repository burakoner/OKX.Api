namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Order State
/// </summary>
public enum OkxAlgoOrderState : byte
{
    /// <summary>
    /// Live
    /// </summary>
    [Map("live")]
    Live = 1,

    /// <summary>
    /// Pause
    /// </summary>
    [Map("pause")]
    Pause = 2,

    /// <summary>
    /// Effective
    /// </summary>
    [Map("effective")]
    Effective = 3,

    /// <summary>
    /// PartiallyEffective
    /// </summary>
    [Map("partially_effective")]
    PartiallyEffective = 4,

    /// <summary>
    /// PartiallyFailed
    /// </summary>
    [Map("partially_failed")]
    PartiallyFailed = 5,

    /// <summary>
    /// Canceled
    /// </summary>
    [Map("canceled")]
    Canceled = 6,

    /// <summary>
    /// Failed
    /// </summary>
    [Map("order_failed")]
    Failed = 7,
}