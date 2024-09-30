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
    Simple,

    /// <summary>
    /// Single Currency Margin
    /// </summary>
    [Map("2")]
    SingleCurrencyMargin,

    /// <summary>
    /// Multi Currency Margin
    /// </summary>
    [Map("3")]
    MultiCurrencyMargin,

    /// <summary>
    /// Portfolio Margin
    /// </summary>
    [Map("4")]
    PortfolioMargin,
}