namespace OKX.Api.Account;

/// <summary>
/// OKX Account Bill Sub-Type
/// </summary>
public enum OkxAccountBillSubType
{
    /// <summary>
    /// Buy
    /// </summary>
    [Map("1")]
    Buy,

    /// <summary>
    /// Sell
    /// </summary>
    [Map("2")]
    Sell,

    /// <summary>
    /// Open Long
    /// </summary>
    [Map("3")]
    OpenLong,

    /// <summary>
    /// Open Short
    /// </summary>
    [Map("4")]
    OpenShort,

    /// <summary>
    /// Close Long
    /// </summary>
    [Map("5")]
    CloseLong,

    /// <summary>
    /// Close Short
    /// </summary>
    [Map("6")]
    CloseShort,

    /// <summary>
    /// Interest Deduction
    /// </summary>
    [Map("9")]
    InterestDeduction,

    /// <summary>
    /// Transfer In
    /// </summary>
    [Map("11")]
    TransferIn,

    /// <summary>
    /// Transfer Out
    /// </summary>
    [Map("12")]
    TransferOut,

    /// <summary>
    /// Interest Deduction For VIP Loans
    /// </summary>
    [Map("14")]
    InterestDeductionForVipLoans,

    /// <summary>
    /// Repay Forcibly
    /// </summary>
    [Map("16")]
    RepayForcibly,

    /// <summary>
    /// Repay Interest By Borrowing Forcibly
    /// </summary>
    [Map("17")]
    RepayInterestByBorrowingForcibly,

    /// <summary>
    /// Partial Liquidation Close Long
    /// </summary>
    [Map("100")]
    PartialLiquidationCloseLong,

    /// <summary>
    /// Partial Liquidation Close Short
    /// </summary>
    [Map("101")]
    PartialLiquidationCloseShort,

    /// <summary>
    /// Partial Liquidation Buy
    /// </summary>
    [Map("102")]
    PartialLiquidationBuy,

    /// <summary>
    /// Partial Liquidation Sell
    /// </summary>
    [Map("103")]
    PartialLiquidationSell,

    /// <summary>
    /// Liquidation Long
    /// </summary>
    [Map("104")]
    LiquidationLong,

    /// <summary>
    /// Liquidation Short
    /// </summary>
    [Map("105")]
    LiquidationShort,

    /// <summary>
    /// Liquidation Buy
    /// </summary>
    [Map("106")]
    LiquidationBuy,

    /// <summary>
    /// Liquidation Sell
    /// </summary>
    [Map("107")]
    LiquidationSell,

    /// <summary>
    /// Clawback
    /// </summary>
    [Map("108")]
    Clawback,

    /// <summary>
    /// Liquidation fees
    /// </summary>
    [Map("109")]
    LiquidationFees,

    /// <summary>
    /// Liquidation Transfer In
    /// </summary>
    [Map("110")]
    LiquidationTransferIn,

    /// <summary>
    /// Liquidation Transfer Out
    /// </summary>
    [Map("111")]
    LiquidationTransferOut,

    /// <summary>
    /// Delivery Long
    /// </summary>
    [Map("113")]
    DeliveryLong,

    /// <summary>
    /// Delivery Short
    /// </summary>
    [Map("113")]
    DeliveryShort,

    /// <summary>
    /// Auto Buy
    /// </summary>
    [Map("114")]
    AutoBuy,

    /// <summary>
    /// Auto Sell
    /// </summary>
    [Map("115")]
    AutoSell,

    /// <summary>
    /// Delivery Exercise Clawback
    /// </summary>
    [Map("117")]
    DeliveryExerciseClawback,

    /// <summary>
    /// System Token Conversion Transfer In
    /// </summary>
    [Map("118")]
    SystemTokenConversionTransferIn,

    /// <summary>
    /// System Token Conversion Transfer Out
    /// </summary>
    [Map("119")]
    SystemTokenConversionTransferOut,

    /// <summary>
    /// ADL Close Long
    /// </summary>
    [Map("125")]
    ADLCloseLong,

    /// <summary>
    /// ADL Close Short
    /// </summary>
    [Map("126")]
    ADLCloseShort,

    /// <summary>
    /// ADL Buy
    /// </summary>
    [Map("127")]
    ADLBuy,

    /// <summary>
    /// ADL Sell
    /// </summary>
    [Map("128")]
    ADLSell,

    /// <summary>
    /// DDH Buy
    /// </summary>
    [Map("131")]
    DDHBuy,

    /// <summary>
    /// DDH Sell
    /// </summary>
    [Map("132")]
    DDHSell,

    /// <summary>
    /// Manual Margin Increase
    /// </summary>
    [Map("160")]
    ManualMarginIncrease,

    /// <summary>
    /// Manual Margin Decrease
    /// </summary>
    [Map("161")]
    ManualMarginDecrease,

    /// <summary>
    /// Auto Margin Increase
    /// </summary>
    [Map("162")]
    AutoMarginIncrease,

    /// <summary>
    /// Exercised
    /// </summary>
    [Map("170")]
    Exercised,

    /// <summary>
    /// Counterparty Exercised
    /// </summary>
    [Map("171")]
    CounterpartyExercised,

    /// <summary>
    /// Expired OTM
    /// </summary>
    [Map("172")]
    ExpiredOTM,

    /// <summary>
    /// Funding Fee Expense
    /// </summary>
    [Map("173")]
    FundingFeeExpense,

    /// <summary>
    /// Funding Fee Income
    /// </summary>
    [Map("174")]
    FundingFeeIncome,

    /// <summary>
    /// System Transfer In
    /// </summary>
    [Map("200")]
    SystemTransferIn,

    /// <summary>
    /// Manually Transfer In
    /// </summary>
    [Map("201")]
    ManuallyTransferIn,

    /// <summary>
    /// System Transfer Out
    /// </summary>
    [Map("202")]
    SystemTransferOut,

    /// <summary>
    /// Manually Transfer Out
    /// </summary>
    [Map("203")]
    ManuallyTransferOut,

    /// <summary>
    /// Block Trade Buy
    /// </summary>
    [Map("204")]
    BlockTradeBuy,

    /// <summary>
    /// Block Trade Sell
    /// </summary>
    [Map("205")]
    BlockTradeSell,

    /// <summary>
    /// Block Trade Open Long
    /// </summary>
    [Map("206")]
    BlockTradeOpenLong,

    /// <summary>
    /// Block Trade Open Short
    /// </summary>
    [Map("207")]
    BlockTradeOpenShort,

    /// <summary>
    /// Block Trade Close Open
    /// </summary>
    [Map("208")]
    BlockTradeCloseOpen,

    /// <summary>
    /// Block Trade Close Short
    /// </summary>
    [Map("209")]
    BlockTradeCloseShort,

    /// <summary>
    /// Manual Borrowing
    /// </summary>
    [Map("210")]
    ManualBorrowing,

    /// <summary>
    /// Manual Repayment
    /// </summary>
    [Map("211")]
    ManualRepayment,

    /// <summary>
    /// Auto Borrow
    /// </summary>
    [Map("212")]
    AutoBorrow,

    /// <summary>
    /// Auto Repay
    /// </summary>
    [Map("213")]
    AutoRepay,

    /// <summary>
    /// Transfer in when using USDT to buy OPTION
    /// </summary>
    [Map("220")]
    TransferInWhenUsingUsdtToBuyOption,

    /// <summary>
    /// Transfer out when using USDT to buy OPTION
    /// </summary>
    [Map("221")]
    TransferOutWhenUsingUsdtToBuyOption,

    /// <summary>
    /// Repayment Transfer In
    /// </summary>
    [Map("224")]
    RepaymentTransferIn,

    /// <summary>
    /// Repayment Transfer Out
    /// </summary>
    [Map("225")]
    RepaymentTransferOut,

    /// <summary>
    /// Easy convert in
    /// </summary>
    [Map("236")]
    EasyConvertIn,

    /// <summary>
    /// Easy convert out
    /// </summary>
    [Map("237")]
    EasyConvertOut,

    /// <summary>
    /// Profit Sharing Expenses
    /// </summary>
    [Map("250")]
    ProfitSharingExpenses,

    /// <summary>
    /// Profit Sharing Refund
    /// </summary>
    [Map("251")]
    ProfitSharingRefund,

    /// <summary>
    /// Profit Sharing Income
    /// </summary>
    [Map("252")]
    ProfitSharingIncome,

    /// <summary>
    /// SpreadTradingBuy
    /// </summary>
    [Map("270")]
    SpreadTradingBuy,

    /// <summary>
    /// SpreadTradingSell
    /// </summary>
    [Map("271")]
    SpreadTradingSell,

    /// <summary>
    /// SpreadTradingOpenLong
    /// </summary>
    [Map("272")]
    SpreadTradingOpenLong,

    /// <summary>
    /// SpreadTradingOpenShort
    /// </summary>
    [Map("273")]
    SpreadTradingOpenShort,

    /// <summary>
    /// SpreadTradingCloseLong
    /// </summary>
    [Map("274")]
    SpreadTradingCloseLong,

    /// <summary>
    /// SpreadTradingCloseShort
    /// </summary>
    [Map("275")]
    SpreadTradingCloseShort,

    /// <summary>
    /// SPOT profit sharing expenses
    /// </summary>
    [Map("280")]
    SpotProfitSharingExpenses,

    /// <summary>
    /// SPOT profit sharing refund
    /// </summary>
    [Map("281")]
    SpotProfitSharingRefund,

    /// <summary>
    /// Copy trade automatic transfer in
    /// </summary>
    [Map("284")]
    CopyTradeAutomaticTransferIn,

    /// <summary>
    /// Copy trade manual transfer in
    /// </summary>
    [Map("285")]
    CopyTradeManualTransferIn,

    /// <summary>
    /// Copy trade automatic transfer out
    /// </summary>
    [Map("286")]
    CopyTradeAutomaticTransferOut,

    /// <summary>
    /// Copy trade manual transfer out
    /// </summary>
    [Map("287")]
    CopyTradeManualTransferOut,

    /// <summary>
    /// Crypto dust auto-transfer out
    /// </summary>
    [Map("290")]
    CryptoDustAutoTransferOut,

    /// <summary>
    /// Fixed loan interest deduction
    /// </summary>
    [Map("293")]
    FixedLoanInterestDeduction,

    /// <summary>
    /// Fixed loan interest refund
    /// </summary>
    [Map("294")]
    FixedLoanInterestRefund,

    /// <summary>
    /// Fixed loan overdue penalty
    /// </summary>
    [Map("295")]
    FixedLoanOverduePenalty,

    /// <summary>
    /// From structured order placements
    /// </summary>
    [Map("296")]
    FromStructuredOrderPlacements,

    /// <summary>
    /// To structured order placements
    /// </summary>
    [Map("297")]
    ToStructuredOrderPlacements,

    /// <summary>
    /// From structured settlements
    /// </summary>
    [Map("298")]
    FromStructuredSettlements,

    /// <summary>
    /// To structured settlements
    /// </summary>
    [Map("299")]
    ToStructuredSettlements
}