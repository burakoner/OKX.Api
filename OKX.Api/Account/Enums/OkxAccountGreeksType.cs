namespace OKX.Api.Account;

/// <summary>
/// OKX Greeks Type
/// </summary>
public enum OkxAccountGreeksType
{
    /// <summary>
    /// GreeksInCoins
    /// </summary>
    [Map("PA")]
    GreeksInCoins,

    /// <summary>
    /// BlackScholesGreeksInDollars
    /// </summary>
    [Map("BS")]
    BlackScholesGreeksInDollars,

    /// <summary>
    /// Empirical Greeks
    /// </summary>
    [Map("CASH")]
    EmpiricalGreeks
}