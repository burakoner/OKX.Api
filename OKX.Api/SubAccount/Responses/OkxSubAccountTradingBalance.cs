namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub Account Trading Balance
/// </summary>
public record OkxSubAccountTradingBalance
{
    /// <summary>
    /// Update Time
    /// </summary>
    [JsonProperty("uTime"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Update Time
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Total Equity
    /// </summary>
    [JsonProperty("totalEq")]
    public decimal TotalEquity { get; set; }

    /// <summary>
    /// Isolated Margin Equity
    /// </summary>
    [JsonProperty("isoEq"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? IsolatedMarginEquity { get; set; }

    /// <summary>
    /// Adjusted Equity
    /// </summary>
    [JsonProperty("adjEq"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? AdjustedEquity { get; set; }

    /// <summary>
    /// Account level available equity, excluding currencies that are restricted due to the collateralized borrowing limit.
    /// Applicable to Multi-currency margin/Portfolio margin
    /// </summary>
    [JsonProperty("availEq"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? AvailableEquity { get; set; }

    /// <summary>
    /// Order Frozen
    /// </summary>
    [JsonProperty("ordFroz"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? OrderFrozen { get; set; }

    /// <summary>
    /// Initial Margin Requirement
    /// </summary>
    [JsonProperty("imr"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// Maintenance Margin Requirement
    /// </summary>
    [JsonProperty("mmr"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MaintenanceMarginRequirement { get; set; }

    /// <summary>
    /// Potential borrowing IMR of the account in USD
    /// Only applicable to Multi-currency margin/Portfolio margin. It is "" for other margin modes.
    /// </summary>
    [JsonProperty("borrowFroz"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? BorrowFrozen { get; set; }

    /// <summary>
    /// Margin Ratio
    /// </summary>
    [JsonProperty("mgnRatio"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// Notional USD
    /// </summary>
    [JsonProperty("notionalUsd"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// Notional value for Borrow in USD
    /// Applicable to Spot mode/Multi-currency margin/Portfolio margin
    /// </summary>
    [JsonProperty("notionalUsdForBorrow"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? NotionalUsdForBorrow { get; set; }

    /// <summary>
    /// Notional value of positions for Perpetual Futures in USD
    /// Applicable to Multi-currency margin/Portfolio margin
    /// </summary>
    [JsonProperty("notionalUsdForSwap"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? NotionalUsdForSwap { get; set; }

    /// <summary>
    /// Notional value of positions for Expiry Futures in USD
    /// Applicable to Multi-currency margin/Portfolio margin
    /// </summary>
    [JsonProperty("notionalUsdForFutures"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? NotionalUsdForFutures { get; set; }

    /// <summary>
    /// Notional value of positions for Option in USD
    /// Applicable to Spot mode/Multi-currency margin/Portfolio margin
    /// </summary>
    [JsonProperty("notionalUsdForOption"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? NotionalUsdForOption { get; set; }

    /// <summary>
    /// Cross-margin unrealized profit and loss at the account level in USD
    /// Applicable to Multi-currency margin/Portfolio margin
    /// </summary>
    [JsonProperty("upl"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Delta (USD)
    /// </summary>
    [JsonProperty("delta"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Delta { get; set; }

    /// <summary>
    /// Delta neutral strategy account level delta leverage
    /// </summary>
    [JsonProperty("deltaLever"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? DeltaLeverage { get; set; }

    /// <summary>
    /// Delta risk status
    /// </summary>
    [JsonProperty("deltaNeutralStatus")]
    public OkxAccountDeltaNeutralStatus? DeltaNeutralStatus { get; set; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public List<OkxSubAccountTradingBalanceDetails> Details { get; set; } = [];
}

/// <summary>
/// OKX Sub Account Trading Balance Detail
/// </summary>
public record OkxSubAccountTradingBalanceDetails
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Equity
    /// </summary>
    [JsonProperty("eq")]
    public decimal? Equity { get; set; }

    /// <summary>
    /// Cash Balance
    /// </summary>
    [JsonProperty("cashBal")]
    public decimal? CashBalance { get; set; }

    /// <summary>
    /// Update Time
    /// </summary>
    [JsonProperty("uTime"), JsonConverter(typeof(LongAsStringNullableConverter))]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Update Time
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Isolated Margin Equity
    /// </summary>
    [JsonProperty("isoEq"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? IsolatedMarginEquity { get; set; }

    /// <summary>
    /// Available Equity
    /// </summary>
    [JsonProperty("availEq"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? AvailableEquity { get; set; }

    /// <summary>
    /// Discount Equity
    /// </summary>
    [JsonProperty("disEq"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? DiscountEquity { get; set; }

    /// <summary>
    /// Frozen balance for Dip Sniper and Peak Sniper
    /// </summary>
    [JsonProperty("fixedBal"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? FixedBalance { get; set; }

    /// <summary>
    /// Available Balance
    /// </summary>
    [JsonProperty("availBal"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? AvailableBalance { get; set; }

    /// <summary>
    /// Frozen Balance
    /// </summary>
    [JsonProperty("frozenBal"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? FrozenBalance { get; set; }

    /// <summary>
    /// Order Frozen
    /// </summary>
    [JsonProperty("ordFrozen"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? OrderFrozen { get; set; }

    /// <summary>
    /// Liabilities
    /// </summary>
    [JsonProperty("liab"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Liability { get; set; }

    /// <summary>
    /// Unrealized Profit and Loss
    /// </summary>
    [JsonProperty("upl"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Unrealized Profit and Loss Liabilities
    /// </summary>
    [JsonProperty("uplLiab"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? UnrealizedProfitAndLossLiability { get; set; }

    /// <summary>
    /// Cross Liabilities
    /// </summary>
    [JsonProperty("crossLiab"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? CrossLiability { get; set; }

    /// <summary>
    /// Trial fund balance
    /// </summary>
    [JsonProperty("rewardBal"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? RewardBalance { get; set; }

    /// <summary>
    /// Isolated Liabilities
    /// </summary>
    [JsonProperty("isoLiab"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? IsolatedLiability { get; set; }

    /// <summary>
    /// Margin Ratio
    /// </summary>
    [JsonProperty("mgnRatio"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// Cross initial margin requirement at the currency level
    /// Applicable to Spot and futures mode and when there is cross position
    /// </summary>
    [JsonProperty("imr"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// Cross maintenance margin requirement at the currency level
    /// Applicable to Spot and futures mode and when there is cross position
    /// </summary>
    [JsonProperty("mmr"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MaintenanceMarginRequirement { get; set; }
    
    /// <summary>
    /// Interest
    /// </summary>
    [JsonProperty("interest"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Interest { get; set; }

    /// <summary>
    /// TWAP
    /// </summary>
    [JsonProperty("twap"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Twap { get; set; }

    /// <summary>
    /// Forced repayment type
    /// </summary>
    [JsonProperty("frpType")]
    public OkxAccountForcedRepaymentType? ForcedRepaymentType { get; set; }

    /// <summary>
    /// Maximum Loan
    /// </summary>
    [JsonProperty("maxLoan"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MaximumLoan { get; set; }

    /// <summary>
    /// USD Equity
    /// </summary>
    [JsonProperty("eqUsd"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? UsdEquity { get; set; }

    /// <summary>
    /// Potential borrowing IMR of currency in USD
    /// Only applicable to Multi-currency margin/Portfolio margin. It is "" for other margin modes.
    /// </summary>
    [JsonProperty("borrowFroz"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? BorrowFrozen { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("notionalLever"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Strategy equity
    /// </summary>
    [JsonProperty("stgyEq"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? StrategyEquity { get; set; }

    /// <summary>
    /// Isolated unrealized profit and loss
    /// </summary>
    [JsonProperty("isoUpl"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? IsolatedUnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Spot in use amount
    /// </summary>
    [JsonProperty("spotInUseAmt"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? SpotInUseAmount { get; set; }

    /// <summary>
    /// User-defined spot risk offset amount
    /// </summary>
    [JsonProperty("clSpotInUseAmt"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? UserDefinedSpotRiskOffsetAmount { get; set; }

    /// <summary>
    /// Max possible spot risk offset amount
    /// </summary>
    [JsonProperty("maxSpotInUse"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MaxPossibleSpotRiskOffsetAmount { get; set; }

    /// <summary>
    /// SPOT isolated balance. only applicable to copy trading
    /// </summary>
    [JsonProperty("spotIsoBal"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? SpotIsolatedBalance { get; set; }

    /// <summary>
    /// Smark sync equity
    /// The default is "0", only applicable to copy trader
    /// </summary>
    [JsonProperty("smtSyncEq"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? SmarkSyncEquity { get; set; }

    /// <summary>
    /// Spot smart sync equity
    /// The default is "0", only applicable to copy trader
    /// </summary>
    [JsonProperty("spotCopyTradingEq"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? SpotCopyTradingEquity { get; set; }

    /// <summary>
    /// Spot balance. The unit is currency, e.g. BTC
    /// </summary>
    [JsonProperty("spotBal"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? SpotBalance { get; set; }

    /// <summary>
    /// Spot average cost price. The unit is USD.
    /// </summary>
    [JsonProperty("openAvgPx"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? SpotAverageCostPrice { get; set; }

    /// <summary>
    /// Spot accumulated cost price. The unit is USD
    /// </summary>
    [JsonProperty("accAvgPx"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? SpotAccumulatedCostPrice { get; set; }

    /// <summary>
    /// Spot unrealized profit and loss. The unit is USD.
    /// </summary>
    [JsonProperty("spotUpl"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? SpotUnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Spot unrealized profit and loss ratio.
    /// </summary>
    [JsonProperty("spotUplRatio"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? SpotUnrealizedProfitAndLossRatio { get; set; }

    /// <summary>
    /// Spot accumulated profit and loss. The unit is USD.
    /// </summary>
    [JsonProperty("totalPnl"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? TotalProfitAndLoss { get; set; }

    /// <summary>
    /// Spot accumulated profit and loss ratio.
    /// </summary>
    [JsonProperty("totalPnlRatio"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? TotalProfitAndLossRatio { get; set; }

    /// <summary>
    /// Platform level collateral restriction status
    /// </summary>
    [JsonProperty("colRes")]
    public string? CollateralRestrictionStatus { get; set; }

    /// <summary>
    /// Risk indicator of auto conversion
    /// </summary>
    [JsonProperty("colBorrAutoConversion")]
    public string? CollateralBorrowAutoConversion { get; set; }

    /// <summary>
    /// Deprecated collateral restriction flag
    /// </summary>
    [JsonProperty("collateralRestrict")]
    public bool? IsCollateralRestricted { get; set; }

    /// <summary>
    /// Whether collateral is enabled
    /// </summary>
    [JsonProperty("collateralEnabled")]
    public bool? IsCollateralEnabled { get; set; }

    /// <summary>
    /// Auto lend status
    /// </summary>
    [JsonProperty("autoLendStatus")]
    public OkxAccountAutoLendStatus? AutoLendStatus { get; set; }

    /// <summary>
    /// Auto lend matched amount
    /// </summary>
    [JsonProperty("autoLendMtAmt"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? AutoLendMatchedAmount { get; set; }
}
