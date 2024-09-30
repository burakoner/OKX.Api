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
    ClosePartially = 1,

    /// <summary>
    /// CloseAll
    /// </summary>
    [Map("2")]
    CloseAll = 2,

    /// <summary>
    /// Liquidation
    /// </summary>
    [Map("3")]
    Liquidation = 3,

    /// <summary>
    /// PartialLiquidation
    /// </summary>
    [Map("4")]
    PartialLiquidation = 4,

    /// <summary>
    /// ADL
    /// </summary>
    [Map("5")]
    ADL = 5
}