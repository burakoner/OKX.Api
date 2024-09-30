namespace OKX.Api.Financial;

/// <summary>
/// OKX Financial OnChainEarn State
/// </summary>
public enum OkxFinancialOnChainEarnOfferState
{
    /// <summary>
    /// Purchasable
    /// </summary>
    [Map("purchasable")]
    Purchasable = 1,

    /// <summary>
    /// Sold Out
    /// </summary>
    [Map("sold_out")]
    SoldOut = 2,

    /// <summary>
    /// Stop
    /// </summary>
    [Map("stop", "Stop")]
    Stop = 3,
}