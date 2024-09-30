namespace OKX.Api.Common;

/// <summary>
/// OKX Closing Position Type
/// </summary>
public enum OkxClosingPositionType
{
    /// <summary>
    /// ClosePartially
    /// </summary>
    [Map("1")]
    ClosePartially,

    /// <summary>
    /// CloseAll
    /// </summary>
    [Map("2")]
    CloseAll,

    /// <summary>
    /// Liquidation
    /// </summary>
    [Map("3")]
    Liquidation,

    /// <summary>
    /// PartialLiquidation
    /// </summary>
    [Map("4")]
    PartialLiquidation,

    /// <summary>
    /// ADL
    /// </summary>
    [Map("5")]
    ADL
}