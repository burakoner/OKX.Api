namespace OKX.Api.Account;

/// <summary>
/// Okx Position Builder
/// </summary>
public record OkxAccountPositionBuilder
{
    /// <summary>
    /// Adjusted equity (USD) for the account
    /// </summary>
    [JsonProperty("eq")]
    public decimal Equity { get; set; }

    /// <summary>
    /// Total MMR (USD) for the account
    /// </summary>
    [JsonProperty("totalMmr")]
    public decimal TotalMmr { get; set; }

    /// <summary>
    /// Total IMR (USD) for the account
    /// </summary>
    [JsonProperty("totalImr")]
    public decimal TotalImr { get; set; }

    /// <summary>
    /// Borrow MMR (USD) for the account
    /// </summary>
    [JsonProperty("borrowMmr")]
    public decimal BorrowMmr { get; set; }

    /// <summary>
    /// Derivatives MMR (USD) for the account
    /// </summary>
    [JsonProperty("derivMmr")]
    public decimal DerivativesMmr { get; set; }

    /// <summary>
    /// Cross margin ratio for the account
    /// </summary>
    [JsonProperty("marginRatio")]
    public decimal MarginRatio { get; set; }

    /// <summary>
    /// UPL for the account
    /// </summary>
    [JsonProperty("upl")]
    public decimal UPL { get; set; }

    /// <summary>
    /// Leverage of the account
    /// </summary>
    [JsonProperty("acctLever")]
    public decimal AccountLeverage { get; set; }

    /// <summary>
    /// Update time for the account, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Update time for the account
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Asset info
    /// </summary>
    [JsonProperty("assets")]
    public List<OkxAccountPositionBuilderAsset> Assets { get; set; } = [];

    /// <summary>
    /// Risk unit data
    /// </summary>
    [JsonProperty("riskUnitData")]
    public List<OkxAccountPositionBuilderRiskUnit> RiskUnitData { get; set; } = [];
}

/// <summary>
/// OKX Position Builder Asset
/// </summary>
public record OkxAccountPositionBuilderAsset
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Available balance
    /// </summary>
    [JsonProperty("availEq")]
    public decimal AvailableEquity { get; set; }

    /// <summary>
    /// Spot in use
    /// </summary>
    [JsonProperty("spotInUse")]
    public decimal SpotInUse { get; set; }

    /// <summary>
    /// Borrow IMR
    /// </summary>
    [JsonProperty("borrowImr")]
    public decimal BorrowImr { get; set; }
}

/// <summary>
/// OKX Position Builder Risk Unit
/// </summary>
public record OkxAccountPositionBuilderRiskUnit
{
    /// <summary>
    /// Risk unit
    /// </summary>
    [JsonProperty("riskUnit")]
    public string RiskUnit { get; set; } = string.Empty;

    /// <summary>
    /// Risk unit MMR before volatility (USD)
    /// Return "" if users don't pass in idxVol
    /// </summary>
    [JsonProperty("mmrBf")]
    public string RiskUnitMMRBeforeVolatility { get; set; } = string.Empty;

    /// <summary>
    /// MMR
    /// </summary>
    [JsonProperty("mmr")]
    public decimal MMR { get; set; }

    /// <summary>
    /// Risk unit IMR before volatility (USD)
    /// Return "" if users don't pass in idxVol
    /// </summary>
    [JsonProperty("imrBf")]
    public string RiskUnitIMRBeforeVolatility { get; set; } = string.Empty;

    /// <summary>
    /// IMR
    /// </summary>
    [JsonProperty("imr")]
    public decimal IMR { get; set; }

    /// <summary>
    /// Risk unit UPL
    /// </summary>
    [JsonProperty("upl")]
    public decimal UPL { get; set; }

    /// <summary>
    /// Stress testing value of spot and volatility (all derivatives, and spot trading in spot-derivatives risk offset mode)
    /// </summary>
    [JsonProperty("mr1")]
    public decimal MR1 { get; set; }

    /// <summary>
    /// Stress testing value of time value of money (TVM) (for options)
    /// </summary>
    [JsonProperty("mr2")]
    public decimal MR2 { get; set; }

    /// <summary>
    /// Stress testing value of volatility span (for options)
    /// </summary>
    [JsonProperty("mr3")]
    public decimal MR3 { get; set; }

    /// <summary>
    /// Stress testing value of basis (for all derivatives)
    /// </summary>
    [JsonProperty("mr4")]
    public decimal MR4 { get; set; }

    /// <summary>
    /// Stress testing value of interest rate risk (for options)
    /// </summary>
    [JsonProperty("mr5")]
    public decimal MR5 { get; set; }

    /// <summary>
    /// Stress testing value of extremely volatile markets (for all derivatives, and spot trading in spot-derivatives risk offset mode)
    /// </summary>
    [JsonProperty("mr6")]
    public decimal MR6 { get; set; }

    /// <summary>
    /// Stress testing value of position reduction cost (for all derivatives)
    /// </summary>
    [JsonProperty("mr7")]
    public decimal MR7 { get; set; }

    /// <summary>
    /// Borrowing MMR/IMR
    /// </summary>
    [JsonProperty("mr8")]
    public decimal MR8 { get; set; }

    /// <summary>
    /// USDT-USDC-USD hedge risk
    /// </summary>
    [JsonProperty("mr9")]
    public decimal MR9 { get; set; }

    /// <summary>
    /// MR1 scenarios
    /// </summary>
    [JsonProperty("mr1Scenarios")]
    public OkxAccountPositionBuilderRiskUnitMR1Scenarios? MR1Scenarios { get; set; }

    /// <summary>
    /// MR1 final result
    /// </summary>
    [JsonProperty("mr1FinalResult")]
    public OkxAccountPositionBuilderRiskUnitMR1FinalResult? MR1FinalResult { get; set; }

    /// <summary>
    /// MR6 final result
    /// </summary>
    [JsonProperty("mr6FinalResult")]
    public OkxAccountPositionBuilderRiskUnitMR6FinalResult? MR6FinalResult { get; set; }

    /// <summary>
    /// Delta
    /// </summary>
    [JsonProperty("delta")]
    public decimal Delta { get; set; }

    /// <summary>
    /// Gamma
    /// </summary>
    [JsonProperty("gamma")]
    public decimal Gamma { get; set; }

    /// <summary>
    /// Theta
    /// </summary>
    [JsonProperty("theta")]
    public decimal Theta { get; set; }

    /// <summary>
    /// Vega
    /// </summary>
    [JsonProperty("vega")]
    public decimal Vega { get; set; }

    /// <summary>
    /// Portfolios
    /// </summary>
    [JsonProperty("portfolios")]
    public List<OkxAccountPositionBuilderRiskUnitPortfolio> Portfolios { get; set; } = [];

    /// <summary>
    /// Position info. Only applicable to Multi-currency margin
    /// </summary>
    [JsonProperty("positions")]
    public List<OkxAccountPositionBuilderRiskUnitPosition> Positions { get; set; } = [];
}

/// <summary>
/// OKX Position Builder Risk Unit MR1 Scenarios
/// </summary>
public record OkxAccountPositionBuilderRiskUnitMR1Scenarios
{
    /// <summary>
    /// Volatility shock down
    /// </summary>
    [JsonProperty("volShockDown")]
    public Dictionary<decimal, decimal> VolatilityShockDown { get; set; } = [];

    /// <summary>
    /// Volatility same
    /// </summary>
    [JsonProperty("volSame")]
    public Dictionary<decimal, decimal> VolatilitySame { get; set; } = [];

    /// <summary>
    /// Volatility shock up
    /// </summary>
    [JsonProperty("volShockUp")]
    public Dictionary<decimal, decimal> VolatilityShockUp { get; set; } = [];
}

/// <summary>
/// OKX Position Builder Risk Unit MR1 Final Result
/// </summary>
public record OkxAccountPositionBuilderRiskUnitMR1FinalResult
{
    /// <summary>
    /// PNL
    /// </summary>
    [JsonProperty("pnl")]
    public decimal PNL { get; set; }

    /// <summary>
    /// Spot shock
    /// </summary>
    [JsonProperty("spotShock")]
    public decimal SpotShock { get; set; }

    /// <summary>
    /// Volatility shock
    /// </summary>
    [JsonProperty("volShock")]
    public string VolatilityShock { get; set; } = string.Empty;
}

/// <summary>
/// OKX Position Builder Risk Unit MR6 Final Result
/// </summary>
public record OkxAccountPositionBuilderRiskUnitMR6FinalResult
{
    /// <summary>
    /// PNL
    /// </summary>
    [JsonProperty("pnl")]
    public decimal PNL { get; set; }

    /// <summary>
    /// Spot shock
    /// </summary>
    [JsonProperty("spotShock")]
    public decimal SpotShock { get; set; }
}

/// <summary>
/// OKX Position Builder Risk Unit Portfolio
/// </summary>
public record OkxAccountPositionBuilderRiskUnitPortfolio
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide")]
    public OkxTradePositionSide PositionSide { get; set; }

    /// <summary>
    /// Average open price
    /// </summary>
    [JsonProperty("avgPx")]
    public decimal? AverageOpenPrice { get; set; }

    /// <summary>
    /// Mark price before price volatility
    /// Return "" if users don't pass in idxVol
    /// </summary>
    [JsonProperty("markPxBf")]
    public decimal? MarkPriceBeforeVolatility { get; set; }

    /// <summary>
    /// Mark price
    /// </summary>
    [JsonProperty("markPx")]
    public decimal? MarkPrice { get; set; }

    /// <summary>
    /// Float P&amp;L
    /// </summary>
    [JsonProperty("floatPnl")]
    public decimal? FloatPnl { get; set; }

    /// <summary>
    /// Notional USD
    /// </summary>
    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// Delta
    /// </summary>
    [JsonProperty("delta")]
    public decimal? Delta { get; set; }

    /// <summary>
    /// Gamma
    /// </summary>
    [JsonProperty("gamma")]
    public decimal? Gamma { get; set; }

    /// <summary>
    /// Theta
    /// </summary>
    [JsonProperty("theta")]
    public decimal? Theta { get; set; }

    /// <summary>
    /// Vega
    /// </summary>
    [JsonProperty("vega")]
    public decimal? Vega { get; set; }

    /// <summary>
    /// Is real position
    /// </summary>
    [JsonProperty("isRealPos")]
    public bool IsRealPosition { get; set; }
}

/// <summary>
/// OKX Position Builder Risk Unit Position
/// </summary>
public record OkxAccountPositionBuilderRiskUnitPosition
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide")]
    public OkxTradePositionSide PositionSide { get; set; }

    /// <summary>
    /// Average open price
    /// </summary>
    [JsonProperty("avgPx")]
    public decimal? AverageOpenPrice { get; set; }

    /// <summary>
    /// Mark price before price volatility
    /// </summary>
    [JsonProperty("markPxBf")]
    public decimal? MarkPriceBeforeVolatility { get; set; }

    /// <summary>
    /// Mark price
    /// </summary>
    [JsonProperty("markPx")]
    public decimal? MarkPrice { get; set; }

    /// <summary>
    /// Float P&amp;L
    /// </summary>
    [JsonProperty("floatPnl")]
    public decimal? FloatPnl { get; set; }

    /// <summary>
    /// IMR (Initial Margin Requirement) before price volatility
    /// </summary>
    [JsonProperty("imr")]
    public decimal? IMRBeforeVolatility { get; set; }

    /// <summary>
    /// IMR (Initial Margin Requirement)
    /// </summary>
    [JsonProperty("imr")]
    public decimal? IMR { get; set; }

    /// <summary>
    /// Margin ratio
    /// </summary>
    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Notional in USD
    /// </summary>
    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// Is real position
    /// </summary>
    [JsonProperty("isRealPos")]
    public bool IsRealPosition { get; set; }
}