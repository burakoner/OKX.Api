namespace OKX.Api.Account;

/// <summary>
/// OKX Account Level
/// </summary>
public enum OkxAccountLevel
{
    /// <summary>
    /// Simple
    /// </summary>
    [Map("1")]
    Simple = 1,

    /// <summary>
    /// Single Currency Margin
    /// </summary>
    [Map("2")]
    SingleCurrencyMargin = 2,

    /// <summary>
    /// Multi Currency Margin
    /// </summary>
    [Map("3")]
    MultiCurrencyMargin = 3,

    /// <summary>
    /// Portfolio Margin
    /// </summary>
    [Map("4")]
    PortfolioMargin = 4,
}