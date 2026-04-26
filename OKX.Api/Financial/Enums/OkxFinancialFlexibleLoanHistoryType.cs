namespace OKX.Api.Financial;

/// <summary>
/// Flexible loan history type
/// </summary>
public enum OkxFinancialFlexibleLoanHistoryType : byte
{
    /// <summary>
    /// Borrowed
    /// </summary>
    [Map("borrowed")]
    Borrowed = 1,

    /// <summary>
    /// Repaid
    /// </summary>
    [Map("repaid")]
    Repaid = 2,

    /// <summary>
    /// Collateral locked
    /// </summary>
    [Map("collateral_locked")]
    CollateralLocked = 3,

    /// <summary>
    /// Collateral released
    /// </summary>
    [Map("collateral_released")]
    CollateralReleased = 4,

    /// <summary>
    /// Forced repayment buy
    /// </summary>
    [Map("forced_repayment_buy")]
    ForcedRepaymentBuy = 5,

    /// <summary>
    /// Forced repayment sell
    /// </summary>
    [Map("forced_repayment_sell")]
    ForcedRepaymentSell = 6,

    /// <summary>
    /// Forced liquidation
    /// </summary>
    [Map("forced_liquidation")]
    ForcedLiquidation = 7,

    /// <summary>
    /// Partial liquidation
    /// </summary>
    [Map("partial_liquidation")]
    PartialLiquidation = 8,

    /// <summary>
    /// Sell collateral
    /// </summary>
    [Map("sell_collateral")]
    SellCollateral = 9,

    /// <summary>
    /// Buy transition coin
    /// </summary>
    [Map("buy_transition_coin")]
    BuyTransitionCoin = 10,

    /// <summary>
    /// Sell transition coin
    /// </summary>
    [Map("sell_transition_coin")]
    SellTransitionCoin = 11,

    /// <summary>
    /// Buy borrowed coin
    /// </summary>
    [Map("buy_borrowed_coin")]
    BuyBorrowedCoin = 12,
}
