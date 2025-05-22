namespace OKX.Api.Account;

/// <summary>
/// OKX Account Mode
/// </summary>
public enum OkxAccountMode : byte
{
    /// <summary>
    /// Simple. Spot Mode
    /// </summary>
    [Map("1")]
    SpotMode = 1,

    /// <summary>
    /// Single Currency Margin
    /// </summary>
    [Map("2")]
    SpotAndFuturesMode = 2,

    /// <summary>
    /// Multi Currency Margin
    /// </summary>
    [Map("3")]
    MultiCurrencyMarginMode = 3,

    /// <summary>
    /// Portfolio Margin
    /// </summary>
    [Map("4")]
    PortfolioMarginMode = 4,
}