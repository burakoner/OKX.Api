namespace OKX.Api.Common;

/// <summary>
/// OKX Quick Margin Type
/// </summary>
public enum OkxQuickMarginType
{
    /// <summary>
    /// Manual
    /// </summary>
    [Map("manual")]
    Manual = 1,

    /// <summary>
    /// AutoBorrow
    /// </summary>
    [Map("auto_borrow")]
    AutoBorrow = 2,

    /// <summary>
    /// AutoRepay
    /// </summary>
    [Map("auto_repay")]
    AutoRepay = 3
}