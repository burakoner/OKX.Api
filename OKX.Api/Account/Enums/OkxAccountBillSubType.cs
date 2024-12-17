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
    Buy = 1,

    /// <summary>
    /// Sell
    /// </summary>
    [Map("2")]
    Sell = 2,

    /// <summary>
    /// Open Long
    /// </summary>
    [Map("3")]
    OpenLong = 3,

    /// <summary>
    /// Open Short
    /// </summary>
    [Map("4")]
    OpenShort = 4,

    /// <summary>
    /// Close Long
    /// </summary>
    [Map("5")]
    CloseLong = 5,

    /// <summary>
    /// Close Short
    /// </summary>
    [Map("6")]
    CloseShort = 6,

    /// <summary>
    /// Interest Deduction
    /// </summary>
    [Map("9")]
    InterestDeduction = 9,

    /// <summary>
    /// Transfer In
    /// </summary>
    [Map("11")]
    TransferIn = 11,

    /// <summary>
    /// Transfer Out
    /// </summary>
    [Map("12")]
    TransferOut = 12,

    /// <summary>
    /// Interest Deduction For VIP Loans
    /// </summary>
    [Map("14")]
    InterestDeductionForVipLoans = 14,

    /// <summary>
    /// Manual Margin Increase
    /// </summary>
    [Map("160")]
    ManualMarginIncrease = 160,

    /// <summary>
    /// Manual Margin Decrease
    /// </summary>
    [Map("161")]
    ManualMarginDecrease = 161,

    /// <summary>
    /// Auto Margin Increase
    /// </summary>
    [Map("162")]
    AutoMarginIncrease = 162,

    /// <summary>
    /// Auto Buy
    /// </summary>
    [Map("114")]
    AutoBuy = 114,

    /// <summary>
    /// Auto Sell
    /// </summary>
    [Map("115")]
    AutoSell = 115,

    /// <summary>
    /// System Token Conversion Transfer In
    /// </summary>
    [Map("118")]
    SystemTokenConversionTransferIn = 118,

    /// <summary>
    /// System Token Conversion Transfer Out
    /// </summary>
    [Map("119")]
    SystemTokenConversionTransferOut = 119,

    /// <summary>
    /// Partial Liquidation Close Long
    /// </summary>
    [Map("100")]
    PartialLiquidationCloseLong = 100,

    /// <summary>
    /// Partial Liquidation Close Short
    /// </summary>
    [Map("101")]
    PartialLiquidationCloseShort = 101,

    /// <summary>
    /// Partial Liquidation Buy
    /// </summary>
    [Map("102")]
    PartialLiquidationBuy = 102,

    /// <summary>
    /// Partial Liquidation Sell
    /// </summary>
    [Map("103")]
    PartialLiquidationSell = 103,

    /// <summary>
    /// Liquidation Long
    /// </summary>
    [Map("104")]
    LiquidationLong = 104,

    /// <summary>
    /// Liquidation Short
    /// </summary>
    [Map("105")]
    LiquidationShort = 105,

    /// <summary>
    /// Liquidation Buy
    /// </summary>
    [Map("106")]
    LiquidationBuy = 106,

    /// <summary>
    /// Liquidation Sell
    /// </summary>
    [Map("107")]
    LiquidationSell = 107,

    /// <summary>
    /// Clawback
    /// </summary>
    [Map("108")]
    Clawback = 108,

    /// <summary>
    /// Liquidation fees
    /// </summary>
    [Map("109")]
    LiquidationFees = 109,

    /// <summary>
    /// Liquidation Transfer In
    /// </summary>
    [Map("110")]
    LiquidationTransferIn = 110,

    /// <summary>
    /// Liquidation Transfer Out
    /// </summary>
    [Map("111")]
    LiquidationTransferOut = 111,

    /// <summary>
    /// ADL Close Long
    /// </summary>
    [Map("125")]
    ADLCloseLong = 125,

    /// <summary>
    /// ADL Close Short
    /// </summary>
    [Map("126")]
    ADLCloseShort = 126,

    /// <summary>
    /// ADL Buy
    /// </summary>
    [Map("127")]
    ADLBuy = 127,

    /// <summary>
    /// ADL Sell
    /// </summary>
    [Map("128")]
    ADLSell = 128,

    /// <summary>
    /// DDH Buy
    /// </summary>
    [Map("131")]
    DDHBuy = 131,

    /// <summary>
    /// DDH Sell
    /// </summary>
    [Map("132")]
    DDHSell = 132,

    /// <summary>
    /// Exercised
    /// </summary>
    [Map("170")]
    Exercised = 170,

    /// <summary>
    /// Counterparty Exercised
    /// </summary>
    [Map("171")]
    CounterpartyExercised = 171,

    /// <summary>
    /// Expired OTM
    /// </summary>
    [Map("172")]
    ExpiredOTM = 172,

    /// <summary>
    /// Delivery Long
    /// </summary>
    [Map("112")]
    DeliveryLong = 112,

    /// <summary>
    /// Delivery Short
    /// </summary>
    [Map("113")]
    DeliveryShort = 113,

    /// <summary>
    /// Delivery Exercise Clawback
    /// </summary>
    [Map("117")]
    DeliveryExerciseClawback = 117,

    /// <summary>
    /// Funding Fee Expense
    /// </summary>
    [Map("173")]
    FundingFeeExpense = 173,

    /// <summary>
    /// Funding Fee Income
    /// </summary>
    [Map("174")]
    FundingFeeIncome = 174,

    /// <summary>
    /// System Transfer In
    /// </summary>
    [Map("200")]
    SystemTransferIn = 200,

    /// <summary>
    /// Manually Transfer In
    /// </summary>
    [Map("201")]
    ManuallyTransferIn = 201,

    /// <summary>
    /// System Transfer Out
    /// </summary>
    [Map("202")]
    SystemTransferOut = 202,

    /// <summary>
    /// Manually Transfer Out
    /// </summary>
    [Map("203")]
    ManuallyTransferOut = 203,

    /// <summary>
    /// Block Trade Buy
    /// </summary>
    [Map("204")]
    BlockTradeBuy = 204,

    /// <summary>
    /// Block Trade Sell
    /// </summary>
    [Map("205")]
    BlockTradeSell = 205,

    /// <summary>
    /// Block Trade Open Long
    /// </summary>
    [Map("206")]
    BlockTradeOpenLong = 206,

    /// <summary>
    /// Block Trade Open Short
    /// </summary>
    [Map("207")]
    BlockTradeOpenShort = 207,

    /// <summary>
    /// Block Trade Close Open
    /// </summary>
    [Map("208")]
    BlockTradeCloseOpen = 208,

    /// <summary>
    /// Block Trade Close Short
    /// </summary>
    [Map("209")]
    BlockTradeCloseShort = 209,

    /// <summary>
    /// Manual Borrowing of quick margin
    /// </summary>
    [Map("210")]
    ManualBorrowingOfQuickMargin = 210,

    /// <summary>
    /// Manual Repayment of quick margin
    /// </summary>
    [Map("211")]
    ManualRepaymentOfQuickMargin = 211,

    /// <summary>
    /// Auto Borrow of quick margin
    /// </summary>
    [Map("212")]
    AutoBorrowOfQuickMargin = 212,

    /// <summary>
    /// Auto Repay of quick margin
    /// </summary>
    [Map("213")]
    AutoRepayOfQuickMargin = 213,

    /// <summary>
    /// Transfer in when using USDT to buy OPTION
    /// </summary>
    [Map("220")]
    TransferInWhenUsingUsdtToBuyOption = 220,

    /// <summary>
    /// Transfer out when using USDT to buy OPTION
    /// </summary>
    [Map("221")]
    TransferOutWhenUsingUsdtToBuyOption = 221,

    /// <summary>
    /// Repay Forcibly
    /// </summary>
    [Map("16")]
    RepayForcibly = 16,

    /// <summary>
    /// Repay Interest By Borrowing Forcibly
    /// </summary>
    [Map("17")]
    RepayInterestByBorrowingForcibly = 17,

    /// <summary>
    /// Repayment Transfer In
    /// </summary>
    [Map("224")]
    RepaymentTransferIn = 224,

    /// <summary>
    /// Repayment Transfer Out
    /// </summary>
    [Map("225")]
    RepaymentTransferOut = 225,

    /// <summary>
    /// Easy convert in
    /// </summary>
    [Map("236")]
    EasyConvertIn = 236,

    /// <summary>
    /// Easy convert out
    /// </summary>
    [Map("237")]
    EasyConvertOut = 237,

    /// <summary>
    /// Profit Sharing Expenses
    /// </summary>
    [Map("250")]
    ProfitSharingExpenses = 250,

    /// <summary>
    /// Profit Sharing Refund
    /// </summary>
    [Map("251")]
    ProfitSharingRefund = 251,

    /// <summary>
    /// Profit Sharing Income
    /// </summary>
    [Obsolete]
    [Map("252")]
    ProfitSharingIncome = 252,

    /// <summary>
    /// SPOT profit sharing expenses
    /// </summary>
    [Map("280")]
    SpotProfitSharingExpenses = 280,

    /// <summary>
    /// SPOT profit sharing refund
    /// </summary>
    [Map("281")]
    SpotProfitSharingRefund = 281,

    /// <summary>
    /// SpreadTradingBuy
    /// </summary>
    [Map("270")]
    SpreadTradingBuy = 270,

    /// <summary>
    /// SpreadTradingSell
    /// </summary>
    [Map("271")]
    SpreadTradingSell = 271,

    /// <summary>
    /// SpreadTradingOpenLong
    /// </summary>
    [Map("272")]
    SpreadTradingOpenLong = 272,

    /// <summary>
    /// SpreadTradingOpenShort
    /// </summary>
    [Map("273")]
    SpreadTradingOpenShort = 273,

    /// <summary>
    /// SpreadTradingCloseLong
    /// </summary>
    [Map("274")]
    SpreadTradingCloseLong = 274,

    /// <summary>
    /// SpreadTradingCloseShort
    /// </summary>
    [Map("275")]
    SpreadTradingCloseShort = 275,

    /// <summary>
    /// Copy trade automatic transfer in
    /// </summary>
    [Map("284")]
    CopyTradeAutomaticTransferIn = 284,

    /// <summary>
    /// Copy trade manual transfer in
    /// </summary>
    [Map("285")]
    CopyTradeManualTransferIn = 285,

    /// <summary>
    /// Copy trade automatic transfer out
    /// </summary>
    [Map("286")]
    CopyTradeAutomaticTransferOut = 286,

    /// <summary>
    /// Copy trade manual transfer out
    /// </summary>
    [Map("287")]
    CopyTradeManualTransferOut = 287,

    /// <summary>
    /// Crypto dust auto-transfer out
    /// </summary>
    [Map("290")]
    CryptoDustAutoTransferOut = 290,

    /// <summary>
    /// Fixed loan interest deduction
    /// </summary>
    [Map("293")]
    FixedLoanInterestDeduction = 293,

    /// <summary>
    /// Fixed loan interest refund
    /// </summary>
    [Map("294")]
    FixedLoanInterestRefund = 294,

    /// <summary>
    /// Fixed loan overdue penalty
    /// </summary>
    [Map("295")]
    FixedLoanOverduePenalty = 295,

    /// <summary>
    /// From structured order placements
    /// </summary>
    [Map("296")]
    FromStructuredOrderPlacements = 296,

    /// <summary>
    /// To structured order placements
    /// </summary>
    [Map("297")]
    ToStructuredOrderPlacements = 297,

    /// <summary>
    /// From structured settlements
    /// </summary>
    [Map("298")]
    FromStructuredSettlements = 298,

    /// <summary>
    /// To structured settlements
    /// </summary>
    [Map("299")]
    ToStructuredSettlements = 299,

    /// <summary>
    /// Manual borrow
    /// </summary>
    [Map("306")]
    ManualBorrow = 306,

    /// <summary>
    /// AutoBorrow
    /// </summary>
    [Map("307")]
    AutoBorrow = 307,

    /// <summary>
    /// ManualRepay
    /// </summary>
    [Map("308")]
    ManualRepay = 308,

    /// <summary>
    /// Auto repay
    /// </summary>
    [Map("309")]
    AutoRepay = 309,

    /// <summary>
    /// Auto offset
    /// </summary>
    [Map("312")]
    AutoOffset = 312,

    /// <summary>
    /// Convert in
    /// </summary>
    [Map("318")]
    ConvertIn = 318,

    /// <summary>
    /// Convert out
    /// </summary>
    [Map("319")]
    ConvertOut = 319,

    /// <summary>
    /// Simple buy
    /// </summary>
    [Map("320")]
    SimpleBuy = 320,

    /// <summary>
    /// Simple sell
    /// </summary>
    [Map("321")]
    SimpleSell = 321,
}