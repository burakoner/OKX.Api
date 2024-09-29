namespace OKX.Api.Account;

/// <summary>
/// OKX Account Bill Sub-Type
/// </summary>
public enum OkxAccountBillSubType
{
    /// <summary>
    /// Buy
    /// </summary>
    Buy,

    /// <summary>
    /// Sell
    /// </summary>
    Sell,

    /// <summary>
    /// Open Long
    /// </summary>
    OpenLong,

    /// <summary>
    /// Open Short
    /// </summary>
    OpenShort,

    /// <summary>
    /// Close Long
    /// </summary>
    CloseLong,

    /// <summary>
    /// Close Short
    /// </summary>
    CloseShort,

    /// <summary>
    /// Interest Deduction
    /// </summary>
    InterestDeduction,

    /// <summary>
    /// Transfer In
    /// </summary>
    TransferIn,

    /// <summary>
    /// Transfer Out
    /// </summary>
    TransferOut,

    /// <summary>
    /// Interest Deduction For VIP Loans
    /// </summary>
    InterestDeductionForVipLoans,

    /// <summary>
    /// Repay Forcibly
    /// </summary>
    RepayForcibly,

    /// <summary>
    /// Repay Interest By Borrowing Forcibly
    /// </summary>
    RepayInterestByBorrowingForcibly,

    /// <summary>
    /// Partial Liquidation Close Long
    /// </summary>
    PartialLiquidationCloseLong,

    /// <summary>
    /// Partial Liquidation Close Short
    /// </summary>
    PartialLiquidationCloseShort,

    /// <summary>
    /// Partial Liquidation Buy
    /// </summary>
    PartialLiquidationBuy,

    /// <summary>
    /// Partial Liquidation Sell
    /// </summary>
    PartialLiquidationSell,

    /// <summary>
    /// Liquidation Long
    /// </summary>
    LiquidationLong,

    /// <summary>
    /// Liquidation Short
    /// </summary>
    LiquidationShort,

    /// <summary>
    /// Liquidation Buy
    /// </summary>
    LiquidationBuy,

    /// <summary>
    /// Liquidation Sell
    /// </summary>
    LiquidationSell,

    /// <summary>
    /// Clawback
    /// </summary>
    Clawback,

    /// <summary>
    /// Liquidation fees
    /// </summary>
    LiquidationFees,

    /// <summary>
    /// Liquidation Transfer In
    /// </summary>
    LiquidationTransferIn,

    /// <summary>
    /// Liquidation Transfer Out
    /// </summary>
    LiquidationTransferOut,

    /// <summary>
    /// Delivery Long
    /// </summary>
    DeliveryLong,

    /// <summary>
    /// Delivery Short
    /// </summary>
    DeliveryShort,

    /// <summary>
    /// Auto Buy
    /// </summary>
    AutoBuy,

    /// <summary>
    /// Auto Sell
    /// </summary>
    AutoSell,

    /// <summary>
    /// Delivery Exercise Clawback
    /// </summary>
    DeliveryExerciseClawback,

    /// <summary>
    /// System Token Conversion Transfer In
    /// </summary>
    SystemTokenConversionTransferIn,

    /// <summary>
    /// System Token Conversion Transfer Out
    /// </summary>
    SystemTokenConversionTransferOut,

    /// <summary>
    /// ADL Close Long
    /// </summary>
    ADLCloseLong,

    /// <summary>
    /// ADL Close Short
    /// </summary>
    ADLCloseShort,

    /// <summary>
    /// ADL Buy
    /// </summary>
    ADLBuy,

    /// <summary>
    /// ADL Sell
    /// </summary>
    ADLSell,

    /// <summary>
    /// DDH Buy
    /// </summary>
    DDHBuy,

    /// <summary>
    /// DDH Sell
    /// </summary>
    DDHSell,

    /// <summary>
    /// Manual Margin Increase
    /// </summary>
    ManualMarginIncrease,

    /// <summary>
    /// Manual Margin Decrease
    /// </summary>
    ManualMarginDecrease,

    /// <summary>
    /// Auto Margin Increase
    /// </summary>
    AutoMarginIncrease,

    /// <summary>
    /// Exercised
    /// </summary>
    Exercised,

    /// <summary>
    /// Counterparty Exercised
    /// </summary>
    CounterpartyExercised,

    /// <summary>
    /// Expired OTM
    /// </summary>
    ExpiredOTM,

    /// <summary>
    /// Funding Fee Expense
    /// </summary>
    FundingFeeExpense,

    /// <summary>
    /// Funding Fee Income
    /// </summary>
    FundingFeeIncome,

    /// <summary>
    /// System Transfer In
    /// </summary>
    SystemTransferIn,

    /// <summary>
    /// Manually Transfer In
    /// </summary>
    ManuallyTransferIn,

    /// <summary>
    /// System Transfer Out
    /// </summary>
    SystemTransferOut,

    /// <summary>
    /// Manually Transfer Out
    /// </summary>
    ManuallyTransferOut,

    /// <summary>
    /// Block Trade Buy
    /// </summary>
    BlockTradeBuy,

    /// <summary>
    /// Block Trade Sell
    /// </summary>
    BlockTradeSell,

    /// <summary>
    /// Block Trade Open Long
    /// </summary>
    BlockTradeOpenLong,

    /// <summary>
    /// Block Trade Open Short
    /// </summary>
    BlockTradeOpenShort,

    /// <summary>
    /// Block Trade Close Open
    /// </summary>
    BlockTradeCloseOpen,

    /// <summary>
    /// Block Trade Close Short
    /// </summary>
    BlockTradeCloseShort,

    /// <summary>
    /// Manual Borrowing
    /// </summary>
    ManualBorrowing,

    /// <summary>
    /// Manual Repayment
    /// </summary>
    ManualRepayment,

    /// <summary>
    /// Auto Borrow
    /// </summary>
    AutoBorrow,

    /// <summary>
    /// Auto Repay
    /// </summary>
    AutoRepay,

    /// <summary>
    /// Transfer in when using USDT to buy OPTION
    /// </summary>
    TransferInWhenUsingUsdtToBuyOption,

    /// <summary>
    /// Transfer out when using USDT to buy OPTION
    /// </summary>
    TransferOutWhenUsingUsdtToBuyOption,

    /// <summary>
    /// Repayment Transfer In
    /// </summary>
    RepaymentTransferIn,

    /// <summary>
    /// Repayment Transfer Out
    /// </summary>
    RepaymentTransferOut,

    /// <summary>
    /// Easy convert in
    /// </summary>
    EasyConvertIn,

    /// <summary>
    /// Easy convert out
    /// </summary>
    EasyConvertOut,

    /// <summary>
    /// Profit Sharing Expenses
    /// </summary>
    ProfitSharingExpenses,

    /// <summary>
    /// Profit Sharing Refund
    /// </summary>
    ProfitSharingRefund,

    /// <summary>
    /// Profit Sharing Income
    /// </summary>
    ProfitSharingIncome,

    /// <summary>
    /// SpreadTradingBuy
    /// </summary>
    SpreadTradingBuy,

    /// <summary>
    /// SpreadTradingSell
    /// </summary>
    SpreadTradingSell,

    /// <summary>
    /// SpreadTradingOpenLong
    /// </summary>
    SpreadTradingOpenLong,

    /// <summary>
    /// SpreadTradingOpenShort
    /// </summary>
    SpreadTradingOpenShort,

    /// <summary>
    /// SpreadTradingCloseLong
    /// </summary>
    SpreadTradingCloseLong,

    /// <summary>
    /// SpreadTradingCloseShort
    /// </summary>
    SpreadTradingCloseShort,

    /// <summary>
    /// SPOT profit sharing expenses
    /// </summary>
    SpotProfitSharingExpenses,

    /// <summary>
    /// SPOT profit sharing refund
    /// </summary>
    SpotProfitSharingRefund,




    /// <summary>
    /// Copy trade automatic transfer in
    /// </summary>
    CopyTradeAutomaticTransferIn,

    /// <summary>
    /// Copy trade manual transfer in
    /// </summary>
    CopyTradeManualTransferIn,

    /// <summary>
    /// Copy trade automatic transfer out
    /// </summary>
    CopyTradeAutomaticTransferOut,

    /// <summary>
    /// Copy trade manual transfer out
    /// </summary>
    CopyTradeManualTransferOut,

    /// <summary>
    /// Crypto dust auto-transfer out
    /// </summary>
    CryptoDustAutoTransferOut,

    /// <summary>
    /// Fixed loan interest deduction
    /// </summary>
    FixedLoanInterestDeduction,

    /// <summary>
    /// Fixed loan interest refund
    /// </summary>
    FixedLoanInterestRefund,

    /// <summary>
    /// Fixed loan overdue penalty
    /// </summary>
    FixedLoanOverduePenalty,

    /// <summary>
    /// From structured order placements
    /// </summary>
    FromStructuredOrderPlacements,

    /// <summary>
    /// To structured order placements
    /// </summary>
    ToStructuredOrderPlacements,

    /// <summary>
    /// From structured settlements
    /// </summary>
    FromStructuredSettlements,

    /// <summary>
    /// To structured settlements
    /// </summary>
    ToStructuredSettlements
}