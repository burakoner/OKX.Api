namespace OKX.Api.CopyTrading;

/// <summary>
/// OkxCopyTradingConfiguration
/// </summary>
public record OkxCopyTradingAccount
{
    /// <summary>
    /// User unique code
    /// </summary>
    [JsonProperty("uniqueCode")]
    public string UniqueCode { get; set; } = string.Empty;

    /// <summary>
    /// Nickname
    /// </summary>
    [JsonProperty("nickName")]
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// Portrait link
    /// </summary>
    [JsonProperty("portLink")]
    public string PortraitLink { get; set; } = string.Empty;

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public List<OkxCopyTradingAccountDetails> Details { get; set; } = [];
}

/// <summary>
/// OkxCopyTradingConfigurationDetails
/// </summary>
public record OkxCopyTradingAccountDetails
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Role type
    /// </summary>
    [JsonProperty("roleType")]
    public OkxCopyTradingRole RoleType { get; set; }

    /// <summary>
    /// Profit sharing ratio.
    /// Only applicable to lead trader, or it will be "". 0.1 represents 10%
    /// </summary>
    [JsonProperty("profitSharingRatio")]
    public decimal ProfitSharingRatio { get; set; }

    /// <summary>
    /// Maximum number of copy traders
    /// </summary>
    [JsonProperty("maxCopyTraderNum")]
    public int MaximumCopyTraderNumber { get; set; }

    /// <summary>
    /// Current number of copy traders
    /// </summary>
    [JsonProperty("copyTraderNum")]
    public int NumberOfCopyTraders { get; set; }
}