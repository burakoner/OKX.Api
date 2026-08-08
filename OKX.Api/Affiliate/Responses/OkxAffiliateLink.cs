namespace OKX.Api.Affiliate;

/// <summary>
/// Affiliate invitation link and its performance.
/// </summary>
public record OkxAffiliateLink
{
    /// <summary>Unique channel ID.</summary>
    [JsonProperty("channelId")]
    public string ChannelId { get; set; } = string.Empty;

    /// <summary>User-defined channel name.</summary>
    [JsonProperty("channelName")]
    public string ChannelName { get; set; } = string.Empty;

    /// <summary>Shareable invitation URL.</summary>
    [JsonProperty("joinLink")]
    public string JoinLink { get; set; } = string.Empty;

    /// <summary>Link type.</summary>
    [JsonProperty("linkType")]
    [JsonConverter(typeof(MapConverter))]
    public OkxAffiliateLinkType LinkType { get; set; }

    /// <summary>Parent inviter commission rate.</summary>
    [JsonProperty("inviterCommissionRate")]
    public decimal InviterCommissionRate { get; set; }

    /// <summary>Co-inviter commission rate, or null for standard links.</summary>
    [JsonProperty("coInviterCommissionRate")]
    public decimal? CoInviterCommissionRate { get; set; }

    /// <summary>Invitee rebate rate configured on the link.</summary>
    [JsonProperty("inviteeDiscountRate")]
    public decimal InviteeDiscountRate { get; set; }

    /// <summary>Number of invitees from the link.</summary>
    [JsonProperty("inviteeCnt")]
    public int InviteeCount { get; set; }

    /// <summary>Number of invitees who traded.</summary>
    [JsonProperty("traderCnt")]
    public int TraderCount { get; set; }

    /// <summary>Cumulative commission in USDT.</summary>
    [JsonProperty("totalCommission")]
    public decimal TotalCommission { get; set; }

    /// <summary>Commission earned in the rolling past 24 hours, in USDT.</summary>
    [JsonProperty("commission24h")]
    public decimal Commission24Hours { get; set; }

    /// <summary>Link creation timestamp, as Unix milliseconds.</summary>
    [JsonProperty("cTime")]
    public long CreationTimestamp { get; set; }

    /// <summary>Link creation time.</summary>
    [JsonIgnore]
    public DateTime CreationTime => CreationTimestamp.ConvertFromMilliseconds();

    /// <summary>Whether this is the default link.</summary>
    [JsonProperty("isDefault")]
    public bool IsDefault { get; set; }

    /// <summary>Link status.</summary>
    [JsonProperty("linkStatus")]
    [JsonConverter(typeof(MapConverter))]
    public OkxAffiliateLinkStatus LinkStatus { get; set; }
}
