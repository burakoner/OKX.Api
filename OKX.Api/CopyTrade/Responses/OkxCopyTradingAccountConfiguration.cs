namespace OKX.Api.CopyTrade;

/// <summary>
/// OkxCopyTradingConfiguration
/// </summary>
public class OkxCopyTradingAccountConfiguration
{
    /// <summary>
    /// User unique code
    /// </summary>
    [JsonProperty("uniqueCode")]
    public string UniqueCode { get; set; } = "";

    /// <summary>
    /// Nickname
    /// </summary>
    [JsonProperty("nickName")]
    public string NickName { get; set; } = "";

    /// <summary>
    /// Portrait link
    /// </summary>
    [JsonProperty("portLink")]
    public string PortraitLink { get; set; } = "";

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public List<OkxCopyTradingAccountConfigurationDetails> Details { get; set; } = [];
}

/// <summary>
/// OkxCopyTradingConfigurationDetails
/// </summary>
public class OkxCopyTradingAccountConfigurationDetails
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Role type
    /// </summary>
    [JsonProperty("roleType"), JsonConverter(typeof(OkxCopyTradingRoleConverter))]
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