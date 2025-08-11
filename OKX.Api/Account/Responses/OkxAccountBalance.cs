using System.Runtime.InteropServices;

namespace OKX.Api.Account;

/// <summary>
/// OKX Account Balance
/// </summary>
public record OkxAccountBalance
{
    /// <summary>
    /// Update time of account information, millisecond format of Unix timestamp, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long UpdateTimestamp { get; set; }

    /// <summary>
    /// Update time of account information
    /// </summary>
    [JsonIgnore]
    public DateTime UpdateTime => UpdateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// The total amount of equity in USD
    /// </summary>
    [JsonProperty("totalEq")]
    public decimal TotalEquity { get; set; }

    /// <summary>
    /// Isolated margin equity in USD
    /// Applicable to Single-currency margin and Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("isoEq")]
    public decimal? IsolatedMarginEquity { get; set; }

    /// <summary>
    /// Adjusted / Effective equity in USD
    /// The net fiat value of the assets in the account that can provide margins for spot, futures, perpetual swap and options under the cross margin mode.
    /// Cause in multi-ccy or PM mode, the asset and margin requirement will all be converted to USD value to process the order check or liquidation.
    /// Due to the volatility of each currency market, our platform calculates the actual USD value of each currency based on discount rates to balance market risks.
    /// Applicable to Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("adjEq")]
    public decimal? AdjustedEquity { get; set; }

    /// <summary>
    /// Account level available equity, excluding currencies that are restricted due to the collateralized borrowing limit.
    /// Applicable to Multi-currency margin/Portfolio margin
    /// </summary>
    [JsonProperty("availEq")]
    public decimal? AvailableEquity { get; set; }

    /// <summary>
    /// Cross margin frozen for pending orders in USD
    /// Only applicable to Multi-currency margin
    /// </summary>
    [JsonProperty("ordFroz")]
    public decimal? OrderFrozen { get; set; }

    /// <summary>
    /// Initial margin requirement in USD
    /// The sum of initial margins of all open positions and pending orders under cross margin mode in USD.
    /// Applicable to Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// Maintenance margin requirement in USD
    /// The sum of maintenance margins of all open positions under cross margin mode in USD.
    /// Applicable to Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    /// <summary>
    /// Potential borrowing IMR of the account in USD
    /// Only applicable to Multi-currency margin and Portfolio margin.It is "" for other margin modes.
    /// </summary>
    [JsonProperty("borrowFroz")]
    public decimal? BorrowFrozen { get; set; }

    /// <summary>
    /// Margin ratio in USD
    /// The index for measuring the risk of a certain asset in the account.
    /// Applicable to Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// Notional value of positions in USD
    /// Applicable to Multi-currency margin and Portfolio margin
    /// </summary>
    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// Notional value for Borrow in USD
    /// Applicable to Spot mode/Multi-currency margin/Portfolio margin
    /// </summary>
    [JsonProperty("notionalUsdForBorrow")]
    public decimal? NotionalUsdForBorrow { get; set; }

    /// <summary>
    /// Notional value of positions for Perpetual Futures in USD
    /// Applicable to Multi-currency margin/Portfolio margin
    /// </summary>
    [JsonProperty("notionalUsdForSwap")]
    public decimal? NotionalUsdForSwap { get; set; }

    /// <summary>
    /// Notional value of positions for Expiry Futures in USD
    /// Applicable to Multi-currency margin/Portfolio margin
    /// </summary>
    [JsonProperty("notionalUsdForFutures")]
    public decimal? NotionalUsdForFutures { get; set; }

    /// <summary>
    /// Notional value of positions for Option in USD
    /// Applicable to Spot mode/Multi-currency margin/Portfolio margin
    /// </summary>
    [JsonProperty("notionalUsdForOption")]
    public decimal? NotionalUsdForOption { get; set; }

    /// <summary>
    /// Cross-margin info of unrealized profit and loss at the account level in USD
    /// Applicable to Multi-currency margin/Portfolio margin
    /// </summary>
    [JsonProperty("upl")]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Detailed asset information in all currencies
    /// </summary>
    [JsonProperty("details")]
    public List<OkxAccountBalanceDetails> Details { get; set; } = [];
}

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
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// Cross maintenance margin requirement at the currency level
    /// Applicable to Spot and futures mode and when there is cross position
    /// </summary>
    [JsonProperty("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

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
    public decimal? TWAP { get; set; }

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
    /// Spot balance. The unit is currency, e.g. BTC.
    /// </summary>
    [JsonProperty("spotBal")]
    public decimal? SpotBalance { get; set; }

    /// <summary>
    /// Spot average cost price. The unit is USD.
    /// </summary>
    [JsonProperty("openAvgPx")]
    public decimal? SpotAverageCostPrice { get; set; }

    /// <summary>
    /// Spot accumulated cost price. The unit is USD. 
    /// </summary>
    [JsonProperty("accAvgPx")]
    public decimal? SpotAccumulatedCostPrice { get; set; }

    /// <summary>
    /// Spot unrealized profit and loss. The unit is USD.
    /// </summary>
    [JsonProperty("spotUpl")]
    public decimal? SpotUnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Spot unrealized profit and loss ratio. 
    /// </summary>
    [JsonProperty("spotUplRatio")]
    public decimal? SpotUnrealizedProfitAndLossRatio { get; set; }

    /// <summary>
    /// Spot accumulated profit and loss. The unit is USD. 
    /// </summary>
    [JsonProperty("totalPnl")]
    public decimal? TotalProfitAndLoss { get; set; }

    /// <summary>
    /// Spot accumulated profit and loss ratio.
    /// </summary>
    [JsonProperty("totalPnlRatio")]
    public decimal? TotalProfitAndLossRatio { get; set; }

    /// <summary>
    /// Platform level collateral restriction status
    /// 0: The restriction is not enabled.
    /// 1: The restriction is not enabled. But the crypto is close to the platform's collateral limit.
    /// 2: The restriction is enabled.This crypto can't be used as margin for your new orders. This may result in failed orders. But it will still be included in the account's adjusted equity and doesn't impact margin ratio.
    /// Refer to Introduction to the platform collateralized borrowing limit for more details.
    /// </summary>
    [JsonProperty("colRes")]
    public string? CollateralRestrictionStatus { get; set; }

    /// <summary>
    /// Indicator of forced repayment when the collateralized borrowing on a crypto reaches the platform limit and users' trading accounts hold this crypto.
    /// Divided into multiple levels from 1-5, the larger the number, the more likely the repayment will be triggered.
    /// The default will be 0, indicating there is no risk currently. 5 means this user is undergoing auto conversion now.
    /// Applicable to Spot mode/Futures mode/Multi-currency margin/Portfolio margin
    /// </summary>
    [JsonProperty("colBorrAutoConversion")]
    public string? CollateralBorrowAutoConversion { get; set; }

    /// <summary>
    /// true: Collateral enabled
    /// false: Collateral disabled
    /// Applicable to Portfolio margin
    /// </summary>
    [JsonProperty("collateralEnabled")]
    public bool? IsCollateralEnabled { get; set; }

    /// <summary>
    /// Auto lend status
    /// unsupported: auto lend is not supported by this currency
    /// off: auto lend is supported but turned off
    /// pending: auto lend is turned on but pending matching
    /// active: auto lend is turned on and matched
    /// </summary>
    [JsonProperty("autoLendStatus")]
    public OkxAccountAutoLendStatus AutoLendStatus { get; set; }

    /// <summary>
    /// Auto lend currency matched amount
    /// Return "0" when autoLendStatus is unsupported/off/pending.Return matched amount when autoLendStatus is active
    /// </summary>
    [JsonProperty("autoLendMtAmt")]
    public decimal? AutoLendmatchedAmount { get; set; }
}