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
    Conditional = 1,

    /// <summary>
    /// OCO
    /// </summary>
    [Map("oco")]
    OCO = 2,

    /// <summary>
    /// Trigger
    /// </summary>
    [Map("trigger")]
    Trigger = 3,

    /// <summary>
    /// TrailingOrder
    /// </summary>
    [Map("move_order_stop")]
    TrailingOrder = 4,

    /// <summary>
    /// Iceberg
    /// </summary>
    [Map("iceberg")]
    Iceberg = 5,

    /// <summary>
    /// TWAP
    /// </summary>
    [Map("twap")]
    TWAP = 6,

    /// <summary>
    /// Chase order, only applicable to FUTURES and SWAP
    /// </summary>
    [Map("chase")]
    Chase = 7,
}