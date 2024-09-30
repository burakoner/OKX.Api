namespace OKX.Api.CopyTrade;

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