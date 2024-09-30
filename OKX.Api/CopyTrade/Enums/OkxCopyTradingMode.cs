namespace OKX.Api.CopyTrade;

/// <summary>
/// OKX Copy Trading Margin Copy Mode
/// </summary>
public enum OkxCopyTradingMode
{
    /// <summary>
    /// Fixed Amount: set the same fixed amount for each order,
    /// </summary>
    [Map("fixed_amount")]
    FixedAmount = 1,

    /// <summary>
    /// Ratio Copy: set amount as a multiple of the lead trader’s order value
    /// </summary>
    [Map("ratio_copy")]
    RatioCopy = 2
}