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
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// Maintenance Margin Requirement
    /// </summary>
    [JsonProperty("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

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
    public List<OkxSubAccountTradingBalanceDetail> Details { get; set; }
}

/// <summary>
/// OKX Sub Account Trading Balance Detail
/// </summary>
public class OkxSubAccountTradingBalanceDetail
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
    public decimal? Liabilities { get; set; }

    /// <summary>
    /// Unrealized Profit and Loss
    /// </summary>
    [JsonProperty("upl")]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Unrealized Profit and Loss Liabilities
    /// </summary>
    [JsonProperty("uplLiab")]
    public decimal? UnrealizedProfitAndLossLiabilities { get; set; }

    /// <summary>
    /// Cross Liabilities
    /// </summary>
    [JsonProperty("crossLiab")]
    public decimal? CrossLiabilities { get; set; }

    /// <summary>
    /// Isolated Liabilities
    /// </summary>
    [JsonProperty("isoLiab")]
    public decimal? IsolatedLiabilities { get; set; }

    /// <summary>
    /// Margin Ratio
    /// </summary>
    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

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
    /// Leverage
    /// </summary>
    [JsonProperty("notionalLever")]
    public decimal? Leverage { get; set; }
}
