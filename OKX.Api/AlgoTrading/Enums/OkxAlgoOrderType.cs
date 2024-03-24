namespace OKX.Api.AlgoTrading.Enums;

/// <summary>
/// OKX Algo Order Type
/// </summary>
public enum OkxAlgoOrderType
{
    /// <summary>
    /// Conditional
    /// </summary>
    Conditional,

    /// <summary>
    /// OCO
    /// </summary>
    OCO,

    /// <summary>
    /// Trigger
    /// </summary>
    Trigger,

    /// <summary>
    /// TrailingOrder
    /// </summary>
    TrailingOrder,

    /// <summary>
    /// Iceberg
    /// </summary>
    Iceberg,

    /// <summary>
    /// TWAP
    /// </summary>
    TWAP,
}