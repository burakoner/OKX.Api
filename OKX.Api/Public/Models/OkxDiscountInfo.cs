namespace OKX.Api.Public.Models;

/// <summary>
/// OKX Discount Info
/// </summary>
public class OkxDiscountInfo
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Discount Level
    /// </summary>
    [JsonProperty("discountLv")]
    public int DiscountLevel { get; set; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("discountInfo")]
    public List<OkxPublicDiscountInfoDetail> Details { get; set; }
}

/// <summary>
/// OKX Public Discount Info Detail
/// </summary>
public class OkxPublicDiscountInfoDetail
{
    /// <summary>
    /// Discount Rate
    /// </summary>
    [JsonProperty("discountRate")]
    public decimal? DiscountRate { get; set; }

    /// <summary>
    /// Maximum Amount
    /// </summary>
    [JsonProperty("maxAmt")]
    public decimal? MaximumAmount { get; set; }

    /// <summary>
    /// Minimum Amount
    /// </summary>
    [JsonProperty("minAmt")]
    public decimal? MinimumAmount { get; set; }
}
