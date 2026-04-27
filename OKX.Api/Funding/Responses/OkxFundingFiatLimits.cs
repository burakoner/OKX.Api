namespace OKX.Api.Funding;

/// <summary>
/// OKX fiat payment limits
/// </summary>
public record OkxFundingFiatLimits
{
    /// <summary>
    /// Daily fiat limit for the selected payment method.
    /// </summary>
    [JsonProperty("dailyLimit")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? DailyLimit { get; set; }

    /// <summary>
    /// Remaining daily fiat limit for the selected payment method.
    /// </summary>
    [JsonProperty("dailyLimitRemaining")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? DailyLimitRemaining { get; set; }

    /// <summary>
    /// Weekly fiat limit for the selected payment method.
    /// </summary>
    [JsonProperty("weeklyLimit")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? WeeklyLimit { get; set; }

    /// <summary>
    /// Remaining weekly fiat limit for the selected payment method.
    /// </summary>
    [JsonProperty("weeklyLimitRemaining")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? WeeklyLimitRemaining { get; set; }

    /// <summary>
    /// Monthly fiat limit for the selected payment method.
    /// </summary>
    [JsonProperty("monthlyLimit")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MonthlyLimit { get; set; }

    /// <summary>
    /// Remaining monthly fiat limit for the selected payment method.
    /// </summary>
    [JsonProperty("monthlyLimitRemaining")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MonthlyLimitRemaining { get; set; }

    /// <summary>
    /// Maximum fiat amount allowed for a single order.
    /// </summary>
    [JsonProperty("maxAmt")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MaximumAmount { get; set; }

    /// <summary>
    /// Minimum fiat amount allowed for a single order.
    /// </summary>
    [JsonProperty("minAmt")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MinimumAmount { get; set; }

    /// <summary>
    /// Lifetime fiat limit when OKX provides it for the payment method.
    /// </summary>
    [JsonProperty("lifetimeLimit")]
    [JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? LifetimeLimit { get; set; }
}
