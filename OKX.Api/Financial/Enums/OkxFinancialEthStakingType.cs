namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial Eth Staking Type
/// </summary>
public enum OkxFinancialEthStakingType
{
    /// <summary>
    /// Redeem
    /// </summary>
    [Map("purchase")]
    Purchase = 1,

    /// <summary>
    /// Redeem
    /// </summary>
    [Map("redeem")]
    Redeem = 2,
}