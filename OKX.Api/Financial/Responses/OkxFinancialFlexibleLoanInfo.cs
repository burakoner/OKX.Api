namespace OKX.Api.Financial;

/// <summary>
/// Flexible loan information
/// </summary>
public record OkxFinancialFlexibleLoanInfo
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("ordId")]
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// Loan notional value in USD
    /// </summary>
    [JsonProperty("loanNotionalUsd"), JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal LoanNotionalUsd { get; set; }

    /// <summary>
    /// Loan data
    /// </summary>
    [JsonProperty("loanData")]
    public List<OkxFinancialFlexibleLoanAsset> LoanData { get; set; } = [];

    /// <summary>
    /// Adjusted collateral value in USD
    /// </summary>
    [JsonProperty("collateralNotionalUsd"), JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal CollateralNotionalUsd { get; set; }

    /// <summary>
    /// Collateral data
    /// </summary>
    [JsonProperty("collateralData")]
    public List<OkxFinancialFlexibleLoanAsset> CollateralData { get; set; } = [];

    /// <summary>
    /// Risk warning data
    /// </summary>
    [JsonProperty("riskWarningData")]
    public OkxFinancialFlexibleLoanRiskWarningData RiskWarningData { get; set; } = new();

    /// <summary>
    /// Current LTV
    /// </summary>
    [JsonProperty("curLTV"), JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal CurrentLtv { get; set; }

    /// <summary>
    /// Margin call LTV
    /// </summary>
    [JsonProperty("marginCallLTV"), JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal MarginCallLtv { get; set; }

    /// <summary>
    /// Liquidation LTV
    /// </summary>
    [JsonProperty("liqLTV"), JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal LiquidationLtv { get; set; }
}

/// <summary>
/// Flexible loan risk warning data
/// </summary>
public record OkxFinancialFlexibleLoanRiskWarningData
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Liquidation price
    /// </summary>
    [JsonProperty("liqPx"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? LiquidationPrice { get; set; }
}
