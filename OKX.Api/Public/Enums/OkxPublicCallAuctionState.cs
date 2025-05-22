namespace OKX.Api.Public;

/// <summary>
/// Call Auction State
/// </summary>
public enum OkxPublicCallAuctionState : byte
{
    /// <summary>
    /// Call Auction
    /// </summary>
    [Map("call_auction")]
    CallAuction = 1,

    /// <summary>
    /// Continuous Trading
    /// </summary>
    [Map("continuous_trading")]
    ContinuousTrading = 2,
}