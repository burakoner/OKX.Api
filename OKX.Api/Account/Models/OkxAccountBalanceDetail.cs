namespace OKX.Api.Account.Models;

/// <summary>
/// OkxAccountBalanceDetail
/// </summary>
public class OkxAccountBalanceDetail
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

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
    public DateTime UpdateTime { get { return UpdateTimestamp.ConvertFromMilliseconds(); } }

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
    /// The sum of the unrealized profit & loss of all margin and derivatives positions of the currency.
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
    /// SPOT isolated balance. only applicable to copy trading
    /// </summary>
    [JsonProperty("spotIsoBal")]
    public decimal? SpotIsolatedBalance { get; set; }
}