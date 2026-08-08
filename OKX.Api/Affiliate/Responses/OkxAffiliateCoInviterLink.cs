namespace OKX.Api.Affiliate;

/// <summary>
/// Co-inviter link on which the authenticated user is the co-inviter.
/// </summary>
public record OkxAffiliateCoInviterLink
{
    /// <summary>Channel ID.</summary>
    [JsonProperty("channelId")]
    public string ChannelId { get; set; } = string.Empty;

    /// <summary>Channel name.</summary>
    [JsonProperty("channelName")]
    public string ChannelName { get; set; } = string.Empty;

    /// <summary>Shareable invitation URL.</summary>
    [JsonProperty("joinLink")]
    public string JoinLink { get; set; } = string.Empty;

    /// <summary>Parent inviter commission rate.</summary>
    [JsonProperty("inviterCommissionRate")]
    public decimal InviterCommissionRate { get; set; }

    /// <summary>Co-inviter commission rate.</summary>
    [JsonProperty("coInviterCommissionRate")]
    public decimal CoInviterCommissionRate { get; set; }

    /// <summary>Invitee rebate rate configured on the link.</summary>
    [JsonProperty("inviteeDiscountRate")]
    public decimal InviteeDiscountRate { get; set; }

    /// <summary>Partially masked partner affiliate username.</summary>
    [JsonProperty("parUserName")]
    public string PartnerUserName { get; set; } = string.Empty;

    /// <summary>Fully masked co-inviter username placeholder.</summary>
    [JsonProperty("coUserName")]
    public string CoInviterUserName { get; set; } = string.Empty;

    /// <summary>Whether the co-inviter is permitted under regional compliance rules.</summary>
    [JsonProperty("isCompliant")]
    public bool IsCompliant { get; set; }

    /// <summary>Optional free-form channel note.</summary>
    [JsonProperty("note")]
    public string Note { get; set; } = string.Empty;

    /// <summary>Whether this is the default co-inviter link.</summary>
    [JsonProperty("isDefault")]
    public bool IsDefault { get; set; }

    /// <summary>Total commission in USDT.</summary>
    [JsonProperty("totalCommission")]
    public decimal TotalCommission { get; set; }

    /// <summary>Commission earned in the rolling past 24 hours, in USDT.</summary>
    [JsonProperty("commission24h")]
    public decimal Commission24Hours { get; set; }

    /// <summary>Number of invitees.</summary>
    [JsonProperty("inviteeCnt")]
    public int InviteeCount { get; set; }

    /// <summary>Number of invitees who traded.</summary>
    [JsonProperty("traderCnt")]
    public int TraderCount { get; set; }

    /// <summary>Number of link clicks.</summary>
    [JsonProperty("clickCnt")]
    public int ClickCount { get; set; }

    /// <summary>Total service fees in USDT.</summary>
    [JsonProperty("totalFee")]
    public decimal TotalFee { get; set; }

    /// <summary>Link creation timestamp, as Unix milliseconds.</summary>
    [JsonProperty("cTime")]
    public long CreationTimestamp { get; set; }

    /// <summary>Link creation time.</summary>
    [JsonIgnore]
    public DateTime CreationTime => CreationTimestamp.ConvertFromMilliseconds();

    /// <summary>Channel assessment result.</summary>
    [JsonProperty("channelAssessmentStatus")]
    public string ChannelAssessmentStatus { get; set; } = string.Empty;

    /// <summary>Parent inviter channel compliance status.</summary>
    [JsonProperty("inviterChannelStatus")]
    public string InviterChannelStatus { get; set; } = string.Empty;

    /// <summary>Co-inviter channel compliance status.</summary>
    [JsonProperty("coInviterChannelStatus")]
    public string CoInviterChannelStatus { get; set; } = string.Empty;

    /// <summary>Link status.</summary>
    [JsonProperty("linkStatus")]
    [JsonConverter(typeof(MapConverter))]
    public OkxAffiliateLinkStatus LinkStatus { get; set; }
}
