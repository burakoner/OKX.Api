namespace OKX.Api.Financial.OnChainEarn;

/// <summary>
/// OKX Financial OnChainEarn State
/// </summary>
public enum OkxFinancialOnChainEarnOfferState
{
    /// <summary>
    /// Purchasable
    /// </summary>
    [Map("purchasable")]
    Purchasable,

    /// <summary>
    /// Sold Out
    /// </summary>
    [Map("sold_out")]
    SoldOut,

    /// <summary>
    /// Stop
    /// </summary>
    [Map("stop", "Stop")]
    Stop,
}