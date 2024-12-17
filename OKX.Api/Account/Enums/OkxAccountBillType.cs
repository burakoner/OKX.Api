namespace OKX.Api.Account;

/// <summary>
/// OKX Account Bill Type
/// </summary>
public enum OkxAccountBillType
{
    /// <summary>
    /// Transfer
    /// </summary>
    [Map("1")]
    Transfer = 1,

    /// <summary>
    /// Trade
    /// </summary>
    [Map("2")]
    Trade = 2,

    /// <summary>
    /// Delivery
    /// </summary>
    [Map("3")]
    Delivery = 3,

    /// <summary>
    /// Auto Token Conversion
    /// </summary>
    [Map("4")]
    AutoTokenConversion = 4,

    /// <summary>
    /// Liquidation
    /// </summary>
    [Map("5")]
    Liquidation = 5,

    /// <summary>
    /// Margin Transfer
    /// </summary>
    [Map("6")]
    MarginTransfer = 6,

    /// <summary>
    /// Interest Deduction
    /// </summary>
    [Map("7")]
    InterestDeduction = 7,

    /// <summary>
    /// Funding Fee
    /// </summary>
    [Map("8")]
    FundingFee = 8,

    /// <summary>
    /// ADL
    /// </summary>
    [Map("9")]
    ADL = 9,

    /// <summary>
    /// Clawback
    /// </summary>
    [Map("10")]
    Clawback = 10,

    /// <summary>
    /// System Token Conversion
    /// </summary>
    [Map("11")]
    SystemTokenConversion = 11,

    /// <summary>
    /// Strategy Transfer
    /// </summary>
    [Map("12")]
    StrategyTransfer = 12,

    /// <summary>
    /// DDH
    /// </summary>
    [Map("13")]
    DDH = 13,

    /// <summary>
    /// Block Trade
    /// </summary>
    [Map("14")]
    BlockTrade = 14,

    /// <summary>
    /// Quick Margin
    /// </summary>
    [Map("15")]
    QuickMargin = 15,

    /// <summary>
    /// Profit Sharing
    /// </summary>
    [Obsolete]
    [Map("18")]
    ProfitSharing = 18,

    /// <summary>
    /// Repay
    /// </summary>
    [Map("22")]
    Repay = 22,

    /// <summary>
    /// SpreadTrading
    /// </summary>
    [Map("24")]
    SpreadTrading = 24,

    /// <summary>
    /// Structured products
    /// </summary>
    [Map("26")]
    StructuredProducts = 26,

    /// <summary>
    /// Convert
    /// </summary>
    [Map("27")]
    Convert = 27,

    /// <summary>
    /// EasyConvert
    /// </summary>
    [Map("28")]
    EasyConvert = 28,

    /// <summary>
    /// One-click repay
    /// </summary>
    [Map("29")]
    OneClickRepay = 29,

    /// <summary>
    /// Simple trade
    /// </summary>
    [Map("30")]
    SimpleTrade = 30,

    /// <summary>
    /// Copy trader profit sharing expenses
    /// </summary>
    [Map("250")]
    CopyTraderProfitSharingExpenses = 250,

    /// <summary>
    /// Copy trader profit sharing refund
    /// </summary>
    [Map("251")]
    CopyTraderProfitSharingRefund = 251,
}