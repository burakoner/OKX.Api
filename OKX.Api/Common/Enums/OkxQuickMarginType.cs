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
    Manual,

    /// <summary>
    /// AutoBorrow
    /// </summary>
    [Map("auto_borrow")]
    AutoBorrow,

    /// <summary>
    /// AutoRepay
    /// </summary>
    [Map("auto_repay")]
    AutoRepay
}