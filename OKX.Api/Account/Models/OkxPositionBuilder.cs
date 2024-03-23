namespace OKX.Api.Account.Models;

/// <summary>
/// Okx Position Builder
/// </summary>
public class OkxPositionBuilder
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
    public List<OkxPositionBuilderAsset> Assets { get; set; }

    [JsonProperty("riskUnitData")]
    public List<OkxPositionBuilderRiskUnit> RiskUnitData { get; set; }
}

public class OkxPositionBuilderAsset
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("availEq")]
    public decimal AvailableEquity { get; set; }

    [JsonProperty("spotInUse")]
    public decimal SpotInUse { get; set; }

    [JsonProperty("borrowMmr")]
    public decimal BorrowMmr { get; set; }

    [JsonProperty("borrowImr")]
    public decimal BorrowImr { get; set; }
}

public class OkxPositionBuilderRiskUnit
{
    [JsonProperty("riskUnit")]
    public string RiskUnit { get; set; }

    [JsonProperty("indexUsd")]
    public string IndexUsd { get; set; }

    [JsonProperty("mmr")]
    public decimal MMR { get; set; }

    [JsonProperty("imr")]
    public decimal IMR { get; set; }

    [JsonProperty("mr1")]
    public decimal MR1 { get; set; }

    [JsonProperty("mr2")]
    public decimal MR2 { get; set; }

    [JsonProperty("mr3")]
    public decimal MR3 { get; set; }

    [JsonProperty("mr4")]
    public decimal MR4 { get; set; }

    [JsonProperty("mr5")]
    public decimal MR5 { get; set; }

    [JsonProperty("mr6")]
    public decimal MR6 { get; set; }

    [JsonProperty("mr7")]
    public decimal MR7 { get; set; }

    [JsonProperty("mr1Scenarios")]
    public OkxPositionBuilderRiskUnitMR1Scenarios MR1Scenarios { get; set; }

    [JsonProperty("mr1FinalResult")]
    public OkxPositionBuilderRiskUnitMR1FinalResult MR1FinalResult { get; set; }

    [JsonProperty("mr6FinalResult")]
    public OkxPositionBuilderRiskUnitMR6FinalResult MR6FinalResult { get; set; }

    [JsonProperty("delta")]
    public decimal Delta { get; set; }

    [JsonProperty("gamma")]
    public decimal Gamma { get; set; }

    [JsonProperty("theta")]
    public decimal Theta { get; set; }

    [JsonProperty("vega")]
    public decimal Vega { get; set; }

    [JsonProperty("portfolios")]
    public List<OkxPositionBuilderRiskUnitPortfolio> Portfolios { get; set; }
}

public class OkxPositionBuilderRiskUnitMR1Scenarios
{
    [JsonProperty("volSame")]
    public Dictionary<decimal, decimal> VolatilitySame { get; set; }

    [JsonProperty("volShockDown")]
    public Dictionary<decimal, decimal> VolatilityShockDown { get; set; }

    [JsonProperty("volShockUp")]
    public Dictionary<decimal, decimal> VolatilityShockUp { get; set; }
}

public class OkxPositionBuilderRiskUnitMR1FinalResult
{
    [JsonProperty("pnl")]
    public decimal PNL { get; set; }

    [JsonProperty("spotShock")]
    public decimal SpotShock { get; set; }

    [JsonProperty("volShock")]
    public string VolatilityShock { get; set; }
}

public class OkxPositionBuilderRiskUnitMR6FinalResult
{
    [JsonProperty("pnl")]
    public decimal PNL { get; set; }

    [JsonProperty("spotShock")]
    public decimal SpotShock { get; set; }
}

public class OkxPositionBuilderRiskUnitPortfolio
{
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    [JsonProperty("notionalUsd")]
    public decimal NotionalUsd { get; set; }

    [JsonProperty("delta")]
    public decimal? Delta { get; set; }

    [JsonProperty("gamma")]
    public decimal? Gamma { get; set; }

    [JsonProperty("theta")]
    public decimal? Theta { get; set; }

    [JsonProperty("vega")]
    public decimal? Vega { get; set; }

    [JsonProperty("isRealPos")]
    public bool IsRealPosition { get; set; }
}
