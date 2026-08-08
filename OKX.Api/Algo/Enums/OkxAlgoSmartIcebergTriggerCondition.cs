namespace OKX.Api.Algo;

/// <summary>
/// Smart iceberg RSI trigger condition.
/// </summary>
public enum OkxAlgoSmartIcebergTriggerCondition : byte
{
    /// <summary>
    /// Cross upward.
    /// </summary>
    [Map("cross_up")]
    CrossUp = 1,

    /// <summary>
    /// Cross downward.
    /// </summary>
    [Map("cross_down")]
    CrossDown = 2,

    /// <summary>
    /// Above the threshold.
    /// </summary>
    [Map("above")]
    Above = 3,

    /// <summary>
    /// Below the threshold.
    /// </summary>
    [Map("below")]
    Below = 4,

    /// <summary>
    /// Cross the threshold in either direction.
    /// </summary>
    [Map("cross")]
    Cross = 5,
}
