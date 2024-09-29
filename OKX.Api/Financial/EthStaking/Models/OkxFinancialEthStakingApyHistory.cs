namespace OKX.Api.Financial.EthStaking;

/// <summary>
/// OKX Financial Eth Staking APY History
/// </summary>
public class OkxFinancialEthStakingApyHistory
{
    /// <summary>
    /// APY(Annual percentage yield), e.g. 0.01 represents 1%
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    /// <summary>
    /// Data time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Data time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
