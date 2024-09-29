namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX Copy Trading Margin Copy Mode
/// </summary>
public enum OkxCopyTradingCopyMode
{
    /// <summary>
    /// Fixed Amount: set the same fixed amount for each order,
    /// </summary>
    FixedAmount,

    /// <summary>
    /// Ratio Copy: set amount as a multiple of the lead trader’s order value
    /// </summary>
    RatioCopy
}