namespace OKX.Api.Affiliate;

/// <summary>
/// Affiliate invitee list item.
/// </summary>
public record OkxAffiliateInviteeListItem
{
    /// <summary>External user UID.</summary>
    [JsonProperty("uid")]
    public string UserId { get; set; } = string.Empty;

    /// <summary>ISO 3166-1 alpha-2 country code.</summary>
    [JsonProperty("country")]
    public string Country { get; set; } = string.Empty;

    /// <summary>Relationship establishment timestamp, as Unix milliseconds.</summary>
    [JsonProperty("joinTime")]
    public long JoinTimestamp { get; set; }

    /// <summary>Relationship establishment time.</summary>
    [JsonIgnore]
    public DateTime JoinTime => JoinTimestamp.ConvertFromMilliseconds();

    /// <summary>First trade timestamp, or null when the invitee has never traded.</summary>
    [JsonProperty("firstTradeTime")]
    public long? FirstTradeTimestamp { get; set; }

    /// <summary>First trade time.</summary>
    [JsonIgnore]
    public DateTime? FirstTradeTime => FirstTradeTimestamp?.ConvertFromMilliseconds();

    /// <summary>Affiliate channel name used at registration.</summary>
    [JsonProperty("channelName")]
    public string ChannelName { get; set; } = string.Empty;

    /// <summary>Effective invitee rebate rate.</summary>
    [JsonProperty("rebateRate")]
    public decimal RebateRate { get; set; }

    /// <summary>Cross-category fee tier rank, from 0 through 13.</summary>
    [JsonProperty("feeTierRank")]
    public int FeeTierRank { get; set; }

    /// <summary>KYC status.</summary>
    [JsonProperty("kycStatus")]
    [JsonConverter(typeof(MapConverter))]
    public OkxAffiliateKycStatus KycStatus { get; set; }

    /// <summary>KYC2 verification timestamp, or null when KYC2 has not been completed.</summary>
    [JsonProperty("kycTime")]
    public long? KycTimestamp { get; set; }

    /// <summary>KYC2 verification time.</summary>
    [JsonIgnore]
    public DateTime? KycTime => KycTimestamp?.ConvertFromMilliseconds();

    /// <summary>Total deposit amount in USDT.</summary>
    [JsonProperty("depAmt")]
    public decimal DepositAmount { get; set; }

    /// <summary>Total trading volume in USDT.</summary>
    [JsonProperty("totalVol")]
    public decimal TotalVolume { get; set; }

    /// <summary>Total trading fees in USDT.</summary>
    [JsonProperty("totalFee")]
    public decimal TotalFee { get; set; }

    /// <summary>Total commission earned from the invitee in USDT.</summary>
    [JsonProperty("totalCommission")]
    public decimal TotalCommission { get; set; }

    /// <summary>Whether the invitee is permitted under regional compliance rules.</summary>
    [JsonProperty("isCompliant")]
    public bool IsCompliant { get; set; }
}
