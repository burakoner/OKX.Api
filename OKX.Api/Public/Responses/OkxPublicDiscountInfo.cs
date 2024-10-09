namespace OKX.Api.Public;

/// <summary>
/// OKX Discount Info
/// </summary>
public record OkxPublicDiscountInfo
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("discountInfo")]
    public List<OkxPublicDiscountInfoData> Info { get; set; } = [];
    
    /// <summary>
    /// 	Minimum discount rate when it exceeds the maximum amount of the last tier.
    /// </summary>
    [JsonProperty("minDiscountRate")]
    public decimal MinimumDiscountRate { get; set; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("discountInfo")]
    public List<OkxPublicDiscountInfoDetails> Details { get; set; } = [];
}

/// <summary>
/// OKX Public Discount Info Detail
/// </summary>
public record OkxPublicDiscountInfoData
{
    /// <summary>
    /// Discount Rate
    /// </summary>
    [JsonProperty("discountRate")]
    public decimal DiscountRate { get; set; }

    /// <summary>
    /// Maximum Amount
    /// </summary>
    [JsonProperty("maxAmt")]
    public decimal MaximumAmount { get; set; }

    /// <summary>
    /// Minimum Amount
    /// </summary>
    [JsonProperty("minAmt")]
    public decimal MinimumAmount { get; set; }
}

/// <summary>
/// OKX Public Discount Info Details
/// </summary>
public record OkxPublicDiscountInfoDetails
{
    /// <summary>
    /// Discount Rate
    /// </summary>
    [JsonProperty("discountRate")]
    public decimal DiscountRate { get; set; }

    /// <summary>
    /// Tier - upper bound, "" means positive infinity
    /// </summary>
    [JsonProperty("maxAmt")]
    public decimal MaximumAmount { get; set; }

    /// <summary>
    /// Tier - lower bound, the minimum is 0
    /// </summary>
    [JsonProperty("minAmt")]
    public decimal MinimumAmount { get; set; }

    /// <summary>
    /// Tiers
    /// </summary>
    [JsonProperty("tier")]
    public int Tiers { get; set; }
    
    /// <summary>
    /// Liquidation penalty rate
    /// </summary>
    [JsonProperty("liqPenaltyRate")]
    public decimal LiquidationPenaltyRate { get; set; }

    /// <summary>
    /// Discount equity in currency for quick calculation if your equity is themaxAmt
    /// </summary>
    [JsonProperty("disCcyEq")]
    public decimal DiscountEquity { get; set; }
}
