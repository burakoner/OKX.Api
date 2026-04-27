namespace OKX.Api.Account;

/// <summary>
/// Trading account configuration update
/// </summary>
public record OkxAccountTradingConfig
{
    /// <summary>
    /// Trading config type
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Strategy type
    /// </summary>
    [JsonProperty("stgyType")]
    public OkxAccountStrategyType StrategyType { get; set; }
}
