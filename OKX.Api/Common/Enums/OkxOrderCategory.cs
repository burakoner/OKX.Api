namespace OKX.Api.Common;

/// <summary>
/// OKX Order Category
/// </summary>
public enum OkxOrderCategory
{
    /// <summary>
    /// Normal
    /// </summary>
    [Map("normal")]
    Normal,

    /// <summary>
    /// TWAP
    /// </summary>
    [Map("twap")]
    TWAP,

    /// <summary>
    /// ADL
    /// </summary>
    [Map("adl")]
    ADL,

    /// <summary>
    /// FullLiquidation
    /// </summary>
    [Map("full_liquidation")]
    FullLiquidation,

    /// <summary>
    /// PartialLiquidation
    /// </summary>
    [Map("partial_liquidation")]
    PartialLiquidation,

    /// <summary>
    /// Delivery
    /// </summary>
    [Map("delivery")]
    Delivery,

    /// <summary>
    /// DDH
    /// </summary>
    [Map("ddh")]
    DDH,
}