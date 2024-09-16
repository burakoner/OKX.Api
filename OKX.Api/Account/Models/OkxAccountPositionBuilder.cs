namespace OKX.Api.Account.Models;

/// <summary>
/// Okx Position Builder
/// </summary>
public class OkxAccountPositionBuilder
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
    /// Update time for the account, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Update time for the account
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

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
public class OkxAccountPositionBuilderAsset
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

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
    /// Borrow in use
    /// </summary>
    [JsonProperty("borrowMmr")]
    public decimal BorrowMmr { get; set; }

    /// <summary>
    /// Borrow IMR
    /// </summary>
    [JsonProperty("borrowImr")]
    public decimal BorrowImr { get; set; }
}

/// <summary>
/// OKX Position Builder Risk Unit
/// </summary>
public class OkxAccountPositionBuilderRiskUnit
{
    /// <summary>
    /// Risk unit
    /// </summary>
    [JsonProperty("riskUnit")]
    public string RiskUnit { get; set; }

    /// <summary>
    /// Index USD
    /// </summary>
    [JsonProperty("indexUsd")]
    public string IndexUsd { get; set; }

    /// <summary>
    /// MMR
    /// </summary>
    [JsonProperty("mmr")]
    public decimal MMR { get; set; }

    /// <summary>
    /// IMR
    /// </summary>
    [JsonProperty("imr")]
    public decimal IMR { get; set; }

    /// <summary>
    /// MR1
    /// </summary>
    [JsonProperty("mr1")]
    public decimal MR1 { get; set; }

    /// <summary>
    /// MR2
    /// </summary>
    [JsonProperty("mr2")]
    public decimal MR2 { get; set; }

    /// <summary>
    /// MR3
    /// </summary>
    [JsonProperty("mr3")]
    public decimal MR3 { get; set; }

    /// <summary>
    /// MR4
    /// </summary>
    [JsonProperty("mr4")]
    public decimal MR4 { get; set; }

    /// <summary>
    /// MR5
    /// </summary>
    [JsonProperty("mr5")]
    public decimal MR5 { get; set; }

    /// <summary>
    /// MR6
    /// </summary>
    [JsonProperty("mr6")]
    public decimal MR6 { get; set; }

    /// <summary>
    /// MR7
    /// </summary>
    [JsonProperty("mr7")]
    public decimal MR7 { get; set; }

    /// <summary>
    /// MR1 scenarios
    /// </summary>
    [JsonProperty("mr1Scenarios")]
    public OkxAccountPositionBuilderRiskUnitMR1Scenarios MR1Scenarios { get; set; }

    /// <summary>
    /// MR1 final result
    /// </summary>
    [JsonProperty("mr1FinalResult")]
    public OkxAccountPositionBuilderRiskUnitMR1FinalResult MR1FinalResult { get; set; }

    /// <summary>
    /// MR6 final result
    /// </summary>
    [JsonProperty("mr6FinalResult")]
    public OkxAccountPositionBuilderRiskUnitMR6FinalResult MR6FinalResult { get; set; }

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
}

/// <summary>
/// OKX Position Builder Risk Unit MR1 Scenarios
/// </summary>
public class OkxAccountPositionBuilderRiskUnitMR1Scenarios
{
    /// <summary>
    /// Volatility shock down
    /// </summary>
    [JsonProperty("volShockDown")]
    public Dictionary<decimal, decimal> VolatilityShockDown { get; set; }

    /// <summary>
    /// Volatility same
    /// </summary>
    [JsonProperty("volSame")]
    public Dictionary<decimal, decimal> VolatilitySame { get; set; }

    /// <summary>
    /// Volatility shock up
    /// </summary>
    [JsonProperty("volShockUp")]
    public Dictionary<decimal, decimal> VolatilityShockUp { get; set; }
}

/// <summary>
/// OKX Position Builder Risk Unit MR1 Final Result
/// </summary>
public class OkxAccountPositionBuilderRiskUnitMR1FinalResult
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
    public string VolatilityShock { get; set; }
}

/// <summary>
/// OKX Position Builder Risk Unit MR6 Final Result
/// </summary>
public class OkxAccountPositionBuilderRiskUnitMR6FinalResult
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
public class OkxAccountPositionBuilderRiskUnitPortfolio
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Notional USD
    /// </summary>
    [JsonProperty("notionalUsd")]
    public decimal NotionalUsd { get; set; }

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
