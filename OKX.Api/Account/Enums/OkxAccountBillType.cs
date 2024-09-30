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
    Transfer,

    /// <summary>
    /// Trade
    /// </summary>
    [Map("2")]
    Trade,

    /// <summary>
    /// Delivery
    /// </summary>
    [Map("3")]
    Delivery,

    /// <summary>
    /// Auto Token Conversion
    /// </summary>
    [Map("4")]
    AutoTokenConversion,

    /// <summary>
    /// Liquidation
    /// </summary>
    [Map("5")]
    Liquidation,

    /// <summary>
    /// Margin Transfer
    /// </summary>
    [Map("6")]
    MarginTransfer,

    /// <summary>
    /// Interest Deduction
    /// </summary>
    [Map("7")]
    InterestDeduction,

    /// <summary>
    /// Funding Fee
    /// </summary>
    [Map("8")]
    FundingFee,

    /// <summary>
    /// ADL
    /// </summary>
    [Map("9")]
    ADL,

    /// <summary>
    /// Clawback
    /// </summary>
    [Map("10")]
    Clawback,

    /// <summary>
    /// System Token Conversion
    /// </summary>
    [Map("11")]
    SystemTokenConversion,

    /// <summary>
    /// Strategy Transfer
    /// </summary>
    [Map("12")]
    StrategyTransfer,

    /// <summary>
    /// DDH
    /// </summary>
    [Map("13")]
    DDH,

    /// <summary>
    /// Block Trade
    /// </summary>
    [Map("14")]
    BlockTrade,

    /// <summary>
    /// Quick Margin
    /// </summary>
    [Map("15")]
    QuickMargin,

    /// <summary>
    /// Profit Sharing
    /// </summary>
    [Map("18")]
    ProfitSharing,

    /// <summary>
    /// Repay
    /// </summary>
    [Map("22")]
    Repay,

    /// <summary>
    /// SpreadTrading
    /// </summary>
    [Map("24")]
    SpreadTrading,

    /// <summary>
    /// Structured products
    /// </summary>
    [Map("26")]
    StructuredProducts,

    /// <summary>
    /// Copy trader profit sharing expenses
    /// </summary>
    [Map("250")]
    CopyTraderProfitSharingExpenses,

    /// <summary>
    /// Copy trader profit sharing refund
    /// </summary>
    [Map("251")]
    CopyTraderProfitSharingRefund,
}