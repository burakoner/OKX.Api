namespace OKX.Api.Account;

/// <summary>
/// OkxAccountBalanceDetails
/// </summary>
public record OkxAccountBalanceDetails
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Equity of the currency
    /// </summary>
    [JsonProperty("eq")]
    public decimal Equity { get; set; }

    /// <summary>
    /// Cash balance
    /// </summary>
    [JsonProperty("cashBal")]
    public decimal CashBalance { get; set; }

    /// <summary>
    /// Update time of currency balance information, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long UpdateTimestamp { get; set; }

    /// <summary>
    /// Update time of currency balance information
    /// </summary>
    [JsonIgnore]
    public DateTime UpdateTime => UpdateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Isolated margin equity of the currency
    /// Applicable to Single-currency margin and Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("isoEq")]
    public decimal? IsolatedMarginEquity { get; set; }

    /// <summary>
    /// Available equity of the currency
    /// The balance that can be used on margin or futures/swap trading.
    /// Applicable to Single-currency margin, Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("availEq")]
    public decimal? AvailableEquity { get; set; }

    /// <summary>
    /// Discount equity of the currency in USD.
    /// </summary>
    [JsonProperty("disEq")]
    public decimal DiscountEquity { get; set; }

    /// <summary>
    /// Frozen balance for Dip Sniper and Peak Sniper
    /// </summary>
    [JsonProperty("fixedBal")]
    public decimal? FixedBalance { get; set; }

    /// <summary>
    /// Available balance of the currency
    /// The balance that can be withdrawn or transferred or used on spot trading.
    /// Applicable to Simple, Single-currency margin, Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("availBal")]
    public decimal AvailableBalance { get; set; }

    /// <summary>
    /// Frozen balance of the currency
    /// </summary>
    [JsonProperty("frozenBal")]
    public decimal FrozenBalance { get; set; }

    /// <summary>
    /// Margin frozen for open orders
    /// </summary>
    [JsonProperty("ordFrozen")]
    public decimal OrderFrozen { get; set; }

    /// <summary>
    /// Liabilities of the currency
    /// Applicable to Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("liab")]
    public decimal? Liabilities { get; set; }

    /// <summary>
    /// The sum of the unrealized profit &amp; loss of all margin and derivatives positions of the currency.
    /// Applicable to Single-currency margin, Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("upl")]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Liabilities due to Unrealized loss of the currency
    /// Applicable to Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("uplLiab")]
    public decimal? UnrealizedProfitAndLossLiabilities { get; set; }

    /// <summary>
    /// Cross liabilities of the currency
    /// Applicable to Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("crossLiab")]
    public decimal? CrossLiabilities { get; set; }

    /// <summary>
    /// Trial fund balance
    /// </summary>
    [JsonProperty("rewardBal")]
    public decimal? RewardBalance { get; set; }

    /// <summary>
    /// Isolated liabilities of the currency
    /// Applicable to Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("isoLiab")]
    public decimal? IsolatedLiabilities { get; set; }

    /// <summary>
    /// Margin ratio of the currency
    /// The index for measuring the risk of a certain asset in the account.
    /// Applicable to Single-currency margin
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
    /// Accrued interest of the currency
    /// Applicable to Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Risk indicator of auto liability repayment
    /// Divided into 5 levels, from 1 to 5, the larger the number, the more likely the auto repayment will be triggered.
    /// Applicable to Multi-currency margin and Portfolio margin and Portfolio margin
    /// </summary>
    [JsonProperty("twap")]
    public decimal? Twap { get; set; }

    /// <summary>
    /// Max loan of the currency
    /// Applicable to cross of Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("maxLoan")]
    public decimal? MaximumLoan { get; set; }

    /// <summary>
    /// Equity in USD of the currency
    /// </summary>
    [JsonProperty("eqUsd")]
    public decimal UsdEquity { get; set; }

    /// <summary>
    /// Potential borrowing IMR of currency in USD
    /// Only applicable to Multi-currency margin and Portfolio margin.It is "" for other margin modes.
    /// </summary>
    [JsonProperty("borrowFroz")]
    public decimal? BorrowFrozen { get; set; }

    /// <summary>
    /// Leverage of the currency
    /// Applicable to Single-currency margin
    /// </summary>
    [JsonProperty("notionalLever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Strategy equity
    /// </summary>
    [JsonProperty("stgyEq")]
    public decimal StrategyEquity { get; set; }

    /// <summary>
    /// Isolated unrealized profit and loss of the currency
    /// Applicable to Single-currency margin and Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("isoUpl")]
    public decimal? IsolatedUnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Spot in use amount
    /// Applicable to Portfolio margin
    /// </summary>
    [JsonProperty("spotInUseAmt")]
    public decimal? SpotInUseAmount { get; set; }

    /// <summary>
    /// User-defined spot risk offset amount
    /// Applicable to Portfolio margin
    /// </summary>
    [JsonProperty("clSpotInUseAmt")]
    public decimal? UserDefinedSpotRiskOffsetAmount { get; set; }

    /// <summary>
    /// Max possible spot risk offset amount
    /// Applicable to Portfolio margin
    /// </summary>
    [JsonProperty("maxSpotInUse")]
    public decimal? MaxPossibleSpotRiskOffsetAmount { get; set; }

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
    public decimal SmarkSyncEquity { get; set; }

    /// <summary>
    /// Spot smart sync equity.
    /// The default is "0", only applicable to copy trader.
    /// </summary>
        [JsonProperty("spotCopyTradingEq")]
    public decimal? SpotCopyTradingEquity { get; set; }

    /// <summary>
    /// 	Spot balance. The unit is currency, e.g. BTC.
    /// </summary>
    [JsonProperty("spotBal")]
    public decimal? SpotBalance { get; set; }

    ///// <summary>
    ///// Spot average cost price. The unit is USD.
    ///// </summary>
    //[JsonProperty("openAvgPx")]
    //public decimal SpotAverageCostPrice { get; set; }

    ///// <summary>
    ///// Spot accumulated cost price. The unit is USD. 
    ///// </summary>
    //[JsonProperty("accAvgPx")]
    //public decimal SpotAccumulatedCostPrice { get; set; }

    /// <summary>
    /// Spot unrealized profit and loss. The unit is USD.
    /// </summary>
    [JsonProperty("spotUpl")]
    public decimal? SpotUpl{ get; set; }

    /// <summary>
    /// Spot unrealized profit and loss ratio. 
    /// </summary>
    [JsonProperty("spotUplRatio")]
    public decimal? SpotUplRatio{ get; set; }

    /// <summary>
    /// Spot accumulated profit and loss. The unit is USD. 
    /// </summary>
    [JsonProperty("totalPnl")]
    public decimal? TotalPnl{ get; set; }

    /// <summary>
    /// Spot accumulated profit and loss ratio.
    /// </summary>
    [JsonProperty("totalPnlRatio")]
    public decimal? TotalPnlRatio { get; set; }
}