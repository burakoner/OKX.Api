namespace OKX.Api.Trade;

/// <summary>
/// OKX Easy Convert Source
/// </summary>
public enum OkxTradeEasyConvertSource
{
    /// <summary>
    /// Trading account
    /// </summary>
    [Map("1")]
    TradingAccount = 1,

    /// <summary>
    /// Funding account
    /// </summary>
    [Map("2")]
    FundingAccount = 2,
}