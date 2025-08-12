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
    /// Platform level collateral restriction status
    /// 0: The restriction is not enabled.
    /// 1: The restriction is not enabled. But the crypto is close to the platform's collateral limit.
    /// 2: The restriction is enabled.This crypto can't be used as margin for your new orders. This may result in failed orders. But it will still be included in the account's adjusted equity and doesn't impact margin ratio.
    /// Refer to Introduction to the platform collateralized borrowing limit for more details.
    /// </summary>
    [JsonProperty("colRes")]
    public OkxPublicCollateralRestrictionStatus CollateralRestrictionStatus { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Minimum discount rate when it exceeds the maximum amount of the last tier.
    /// </summary>
    [JsonProperty("minDiscountRate")]
    public decimal MinimumDiscountRate { get; set; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public List<OkxPublicDiscountInfoDetails> Details { get; set; } = [];
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
