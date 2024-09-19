namespace OKX.Api.SubAccount.Models;

/// <summary>
/// OKX Sub Account Trading Balance
/// </summary>
public class OkxSubAccountTradingBalance
{
    /// <summary>
    /// Update Time
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Update Time
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime { get { return UpdateTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Total Equity
    /// </summary>
    [JsonProperty("totalEq")]
    public decimal TotalEquity { get; set; }

    /// <summary>
    /// Isolated Margin Equity
    /// </summary>
    [JsonProperty("isoEq")]
    public decimal? IsolatedMarginEquity { get; set; }

    /// <summary>
    /// Adjusted Equity
    /// </summary>
    [JsonProperty("adjEq")]
    public decimal? AdjustedEquity { get; set; }

    /// <summary>
    /// Order Frozen
    /// </summary>
    [JsonProperty("ordFroz")]
    public decimal? OrderFrozen { get; set; }

    /// <summary>
    /// Initial Margin Requirement
    /// </summary>
    [JsonProperty("imr")]
    public decimal? IMR { get; set; }

    /// <summary>
    /// Maintenance Margin Requirement
    /// </summary>
    [JsonProperty("mmr")]
    public decimal? MMR { get; set; }

    /// <summary>
    /// Potential borrowing IMR of the account in USD
    /// Only applicable to Multi-currency margin/Portfolio margin. It is "" for other margin modes.
    /// </summary>
    [JsonProperty("borrowFroz")]
    public decimal? BorrowFrozen { get; set; }

    /// <summary>
    /// Margin Ratio
    /// </summary>
    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// Notional USD
    /// </summary>
    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public List<OkxSubAccountTradingBalanceDetails> Details { get; set; }
}

/// <summary>
/// OKX Sub Account Trading Balance Detail
/// </summary>
public class OkxSubAccountTradingBalanceDetails
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

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
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Update Time
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime { get { return UpdateTimestamp?.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Isolated Margin Equity
    /// </summary>
    [JsonProperty("isoEq")]
    public decimal? IsolatedMarginEquity { get; set; }

    /// <summary>
    /// Available Equity
    /// </summary>
    [JsonProperty("availEq")]
    public decimal? AvailableEquity { get; set; }

    /// <summary>
    /// Discount Equity
    /// </summary>
    [JsonProperty("disEq")]
    public decimal? DiscountEquity { get; set; }

    /// <summary>
    /// Available Balance
    /// </summary>
    [JsonProperty("availBal")]
    public decimal? AvailableBalance { get; set; }

    /// <summary>
    /// Frozen Balance
    /// </summary>
    [JsonProperty("frozenBal")]
    public decimal? FrozenBalance { get; set; }

    /// <summary>
    /// Order Frozen
    /// </summary>
    [JsonProperty("ordFrozen")]
    public decimal? OrderFrozen { get; set; }

    /// <summary>
    /// Liabilities
    /// </summary>
    [JsonProperty("liab")]
    public decimal? Liability { get; set; }

    /// <summary>
    /// Unrealized Profit and Loss
    /// </summary>
    [JsonProperty("upl")]
    public decimal? UPL { get; set; }

    /// <summary>
    /// Unrealized Profit and Loss Liabilities
    /// </summary>
    [JsonProperty("uplLiab")]
    public decimal? UplLiability { get; set; }

    /// <summary>
    /// Cross Liabilities
    /// </summary>
    [JsonProperty("crossLiab")]
    public decimal? CrossLiability { get; set; }

    /// <summary>
    /// Isolated Liabilities
    /// </summary>
    [JsonProperty("isoLiab")]
    public decimal? IsolatedLiability { get; set; }

    /// <summary>
    /// Margin Ratio
    /// </summary>
    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// Cross initial margin requirement at the currency level
    /// Applicable to Spot and futures mode and when there is cross position
    /// </summary>
    [JsonProperty("imr")]
    public decimal? IMR { get; set; }

    /// <summary>
    /// Cross maintenance margin requirement at the currency level
    /// Applicable to Spot and futures mode and when there is cross position
    /// </summary>
    [JsonProperty("mmr")]
    public decimal? MMR { get; set; }
    
    /// <summary>
    /// Interest
    /// </summary>
    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// TWAP
    /// </summary>
    [JsonProperty("twap")]
    public decimal? Twap { get; set; }

    /// <summary>
    /// Maximum Loan
    /// </summary>
    [JsonProperty("maxLoan")]
    public decimal? MaximumLoan { get; set; }

    /// <summary>
    /// USD Equity
    /// </summary>
    [JsonProperty("eqUsd")]
    public decimal? UsdEquity { get; set; }

    /// <summary>
    /// Potential borrowing IMR of currency in USD
    /// Only applicable to Multi-currency margin/Portfolio margin. It is "" for other margin modes.
    /// </summary>
    [JsonProperty("borrowFroz")]
    public decimal? BorrowFrozen { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("notionalLever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// SPOT isolated balance. only applicable to copy trading
    /// </summary>
    [JsonProperty("spotIsoBal")]
    public decimal? SpotIsolatedBalance { get; set; }

    /// <summary>
    /// Smark sync equity
    /// The default is "0", only applicable to copy trader
    /// </summary>
    [JsonProperty("smtSyncEq")]
    public decimal? SmarkSyncEquity { get; set; }

    /// <summary>
    /// Spot balance. The unit is currency, e.g. BTC
    /// </summary>
    [JsonProperty("spotBal")]
    public decimal? SpotBalance { get; set; }

    /// <summary>
    /// Spot average cost price. The unit is USD.
    /// </summary>
    [JsonProperty("openAvgPx")]
    public decimal? SpotAverageCostPrice { get; set; }

    /// <summary>
    /// Spot accumulated cost price. The unit is USD
    /// </summary>
    [JsonProperty("accAvgPx")]
    public decimal? SpotAccumulatedCostPrice { get; set; }

    /// <summary>
    /// Spot unrealized profit and loss. The unit is USD.
    /// </summary>
    [JsonProperty("spotUpl")]
    public decimal? SpotUpl { get; set; }

    /// <summary>
    /// Spot unrealized profit and loss ratio.
    /// </summary>
    [JsonProperty("spotUplRatio")]
    public decimal? SpotUplRatio { get; set; }

    /// <summary>
    /// Spot accumulated profit and loss. The unit is USD.
    /// </summary>
    [JsonProperty("totalPnl")]
    public decimal? TotalPnl { get; set; }

    /// <summary>
    /// Spot accumulated profit and loss ratio.
    /// </summary>
    [JsonProperty("totalPnlRatio")]
    public decimal? TotalPnlRatio { get; set; }
}
