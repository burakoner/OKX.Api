namespace OKX.Api.Affiliate;

/// <summary>
/// Sub-affiliate and lifetime performance metrics.
/// </summary>
public record OkxAffiliateSubAffiliate
{
    /// <summary>External UID of the sub-affiliate.</summary>
    [JsonProperty("subAffiliateUid")]
    public string UserId { get; set; } = string.Empty;

    /// <summary>ISO 3166-1 alpha-2 country code.</summary>
    [JsonProperty("country")]
    public string Country { get; set; } = string.Empty;

    /// <summary>Sub-affiliate registration timestamp, as Unix milliseconds.</summary>
    [JsonProperty("joinTime")]
    public long JoinTimestamp { get; set; }

    /// <summary>Sub-affiliate registration time.</summary>
    [JsonIgnore]
    public DateTime JoinTime => JoinTimestamp.ConvertFromMilliseconds();

    /// <summary>Depth relative to the authenticated affiliate: 2 for direct, 3 for indirect.</summary>
    [JsonProperty("subAffiliateLevel")]
    public int Level { get; set; }

    /// <summary>Sub-affiliate commission rate.</summary>
    [JsonProperty("commissionRate")]
    public decimal CommissionRate { get; set; }

    /// <summary>Whether the sub-affiliate is permitted under regional compliance rules.</summary>
    [JsonProperty("isCompliant")]
    public bool IsCompliant { get; set; }

    /// <summary>Number of direct invitees.</summary>
    [JsonProperty("inviteeCnt")]
    public int InviteeCount { get; set; }

    /// <summary>Number of invitees who traded.</summary>
    [JsonProperty("traderCnt")]
    public int TraderCount { get; set; }

    /// <summary>Total deposit from invitees in USDT.</summary>
    [JsonProperty("depAmt")]
    public decimal DepositAmount { get; set; }

    /// <summary>Total trading volume from invitees in USDT.</summary>
    [JsonProperty("totalVol")]
    public decimal TotalVolume { get; set; }

    /// <summary>Total trading fees from invitees in USDT.</summary>
    [JsonProperty("totalFee")]
    public decimal TotalFee { get; set; }

    /// <summary>Commission earned from the sub-affiliate's invitees in USDT.</summary>
    [JsonProperty("totalCommission")]
    public decimal TotalCommission { get; set; }
}
