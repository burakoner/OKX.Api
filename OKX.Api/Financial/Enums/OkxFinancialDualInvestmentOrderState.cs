namespace OKX.Api.Financial;

/// <summary>
/// OKX Dual Investment Order State
/// </summary>
public enum OkxFinancialDualInvestmentOrderState : byte
{
    /// <summary>
    /// Request has been received by system, will further process
    /// </summary>
    [Map("initial")]
    Initial = 1,

    /// <summary>
    /// Trade received by liquidity provider, pending further processing
    /// </summary>
    [Map("pending_book")]
    PendingBook = 2,

    /// <summary>
    /// Order or trade is live
    /// </summary>
    [Map("live")]
    Live = 3,

    /// <summary>
    /// Order or trade has been rejected
    /// </summary>
    [Map("rejected")]
    Rejected = 4,

    /// <summary>
    /// Waiting for settlement
    /// </summary>
    [Map("pending_settle")]
    PendingSettle = 5,

    /// <summary>
    /// Settlement completed
    /// </summary>
    [Map("settled")]
    Settled = 6,

    /// <summary>
    /// Redeem received, waiting for liquidity provider further processing
    /// </summary>
    [Map("pending_redeem_booking")]
    PendingRedeemBooking = 7,

    /// <summary>
    /// Liquidity provider booked, waiting for transfer
    /// </summary>
    [Map("pending_redeem")]
    PendingRedeem = 8,

    /// <summary>
    /// Redemption in progress
    /// </summary>
    [Map("redeeming")]
    Redeeming = 9,

    /// <summary>
    /// Redemption completed
    /// </summary>
    [Map("redeemed")]
    Redeemed = 10,
}
