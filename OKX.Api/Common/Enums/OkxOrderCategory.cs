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
    Normal = 1,

    /// <summary>
    /// TWAP
    /// </summary>
    [Map("twap")]
    TWAP = 2,

    /// <summary>
    /// ADL
    /// </summary>
    [Map("adl")]
    ADL = 3,

    /// <summary>
    /// FullLiquidation
    /// </summary>
    [Map("full_liquidation")]
    FullLiquidation = 4,

    /// <summary>
    /// PartialLiquidation
    /// </summary>
    [Map("partial_liquidation")]
    PartialLiquidation = 5,

    /// <summary>
    /// Delivery
    /// </summary>
    [Map("delivery")]
    Delivery = 6,

    /// <summary>
    /// DDH
    /// </summary>
    [Map("ddh")]
    DDH = 7,
}