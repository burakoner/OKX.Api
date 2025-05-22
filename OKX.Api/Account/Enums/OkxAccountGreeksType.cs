namespace OKX.Api.Account;

/// <summary>
/// OKX Greeks Type
/// </summary>
public enum OkxAccountGreeksType : byte
{
    /// <summary>
    /// GreeksInCoins
    /// </summary>
    [Map("PA")]
    GreeksInCoins = 1,

    /// <summary>
    /// BlackScholesGreeksInDollars
    /// </summary>
    [Map("BS")]
    BlackScholesGreeksInDollars = 2,

    /// <summary>
    /// Empirical Greeks
    /// </summary>
    [Map("CASH")]
    EmpiricalGreeks = 3
}