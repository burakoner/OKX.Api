namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Order Type
/// </summary>
public enum OkxAlgoOrderType
{
    /// <summary>
    /// Conditional
    /// </summary>
    [Map("conditional")]
    Conditional,

    /// <summary>
    /// OCO
    /// </summary>
    [Map("oco")]
    OCO,

    /// <summary>
    /// Trigger
    /// </summary>
    [Map("trigger")]
    Trigger,

    /// <summary>
    /// TrailingOrder
    /// </summary>
    [Map("move_order_stop")]
    TrailingOrder,

    /// <summary>
    /// Iceberg
    /// </summary>
    [Map("iceberg")]
    Iceberg,

    /// <summary>
    /// TWAP
    /// </summary>
    [Map("twap")]
    TWAP,
}