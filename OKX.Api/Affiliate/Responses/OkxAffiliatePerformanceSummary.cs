namespace OKX.Api.Affiliate;

/// <summary>
/// Aggregated affiliate performance metrics.
/// </summary>
public record OkxAffiliatePerformanceSummary
{
    /// <summary>Latest data snapshot timestamp, as Unix milliseconds.</summary>
    [JsonProperty("uTime")]
    public long UpdateTimestamp { get; set; }

    /// <summary>Latest data snapshot time.</summary>
    [JsonIgnore]
    public DateTime UpdateTime => UpdateTimestamp.ConvertFromMilliseconds();

    /// <summary>Total number of invitees.</summary>
    [JsonProperty("inviteeCnt")]
    public int InviteeCount { get; set; }

    /// <summary>Total deposit amount in USDT for the selected window.</summary>
    [JsonProperty("depAmt")]
    public decimal DepositAmount { get; set; }

    /// <summary>Per-category performance metrics.</summary>
    [JsonProperty("details")]
    public List<OkxAffiliatePerformanceDetail> Details { get; set; } = [];
}

/// <summary>
/// Affiliate performance metrics for one commission category.
/// </summary>
public record OkxAffiliatePerformanceDetail
{
    /// <summary>Commission calculation category.</summary>
    [JsonProperty("commissionCategory")]
    [JsonConverter(typeof(MapConverter))]
    public OkxAffiliateCommissionCategory CommissionCategory { get; set; }

    /// <summary>Invitees whose lifetime-first trade occurred in the selected window.</summary>
    [JsonProperty("firstTraderCnt")]
    public int FirstTraderCount { get; set; }

    /// <summary>Invitees who traded in this category in the selected window.</summary>
    [JsonProperty("traderCnt")]
    public int TraderCount { get; set; }

    /// <summary>Trading volume in USDT for the selected window.</summary>
    [JsonProperty("vol")]
    public decimal Volume { get; set; }

    /// <summary>Commission in USDT for the selected window.</summary>
    [JsonProperty("commission")]
    public decimal Commission { get; set; }
}
