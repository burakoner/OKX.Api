namespace OKX.Api.Affiliate;

/// <summary>
/// OKX Affiliate Invitee
/// </summary>
public class OkxAffiliateInvitee
{
    /// <summary>
    /// Invitee's relative level to the affiliate. If the user is a invitee, the level will be 2.
    /// </summary>
    [JsonProperty("inviteeLv")]
    public int InviteeLevel { get; set; }
    
    /// <summary>
    /// Timestamp that the rebate relationship is established, Unix timestamp in millisecond format, e.g. 1597026383085
    /// </summary>
    [JsonProperty("joinTime")]
    public long JoinTimestamp { get; set; }

    /// <summary>
    /// Timestamp that the rebate relationship is established, Unix timestamp in millisecond format, e.g. 1597026383085
    /// </summary>
    [JsonIgnore]
    public DateTime JoinTime { get => JoinTimestamp.ConvertFromMilliseconds(); }

    /// <summary>
    /// Self rebate rate of the invitee (in decimal), e.g. 0.01 represents 10%
    /// </summary>
    [JsonProperty("inviteeRebateRate")]
    public decimal InviteeRebateRate { get; set; }

    /// <summary>
    /// Total commission earned from the invitee, unit in USDT
    /// </summary>
    [JsonProperty("totalCommission")]
    public decimal TotalCommission { get; set; }

    /// <summary>
    /// Timestamp that the first trade is completed after the latest rebate relationship is established with the parent affiliate
    /// Unix timestamp in millisecond format, e.g. 1597026383085
    /// If user has not traded, "" will be returned
    /// </summary>
    [JsonProperty("firstTradeTime")]
    public long? FirstTradeTimestamp { get; set; }
    
    /// <summary>
    /// Timestamp that the first trade is completed after the latest rebate relationship is established with the parent affiliate
    /// Unix timestamp in millisecond format, e.g. 1597026383085
    /// If user has not traded, "" will be returned
    /// </summary>
    [JsonIgnore]
    public DateTime? FirstTradeTime { get => FirstTradeTimestamp?.ConvertFromMilliseconds(); }

    /// <summary>
    /// Invitee trading fee level, e.g. Lv1
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; } = "";

    /// <summary>
    /// Accumulated amount of deposit in USDT
    /// If user has not deposited, 0 will be returned
    /// </summary>
    [JsonProperty("depAmt")]
    public decimal AccumulatedDepositAmount { get; set; }

    /// <summary>
    /// Accumulated Trading volume in the current month in USDT
    /// If user has not traded, 0 will be returned
    /// </summary>
    [JsonProperty("volMonth")]
    public decimal AccumulatedVolumeMonth { get; set; }

    /// <summary>
    /// Accumulated Amount of trading fee in USDT
    /// If there is no any fee, 0 will be returned
    /// </summary>
    [JsonProperty("accFee")]
    public decimal AccumulatedFee { get; set; }

    /// <summary>
    /// KYC2 verification time. Unix timestamp in millisecond format and the precision is in day
    /// If user has not passed KYC2, "" will be returned
    /// </summary>
    [JsonProperty("kycTime")]
    public long? KycTimestamp { get; set; }

    /// <summary>
    /// KYC2 verification time. Unix timestamp in millisecond format and the precision is in day
    /// If user has not passed KYC2, "" will be returned
    /// </summary>
    [JsonIgnore]
    public DateTime? KycTime { get => KycTimestamp?.ConvertFromMilliseconds(); }
    
    /// <summary>
    /// User country or region. e.g. "United Kingdom"
    /// </summary>
    [JsonProperty("region")]
    public string Region { get; set; } = "";
    
    /// <summary>
    /// Affiliate invite code that the invitee registered/recalled via
    /// </summary>
    [JsonProperty("affiliateCode")]
    public string AffiliateCode { get; set; } = "";
}
