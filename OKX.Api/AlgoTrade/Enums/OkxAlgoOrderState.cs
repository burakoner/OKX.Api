namespace OKX.Api.AlgoTrade.Enums;

/// <summary>
/// OKX Algo Order State
/// </summary>
public enum OkxAlgoOrderState
{
    /// <summary>
    /// Live
    /// </summary>
    Live,

    /// <summary>
    /// Pause
    /// </summary>
    Pause,

    /// <summary>
    /// Effective
    /// </summary>
    Effective,

    /// <summary>
    /// PartiallyEffective
    /// </summary>
    PartiallyEffective,

    /// <summary>
    /// PartiallyFailed
    /// </summary>
    PartiallyFailed,

    /// <summary>
    /// Canceled
    /// </summary>
    Canceled,

    /// <summary>
    /// Failed
    /// </summary>
    Failed,
}