namespace OKX.Api.Rubik;

/// <summary>
/// OKX Support Coins
/// </summary>
public class OkxRubikSupportCoins
{
    /// <summary>
    /// Contract
    /// </summary>
    [JsonProperty("contract")]
    public List<string> Contract { get; set; } = [];

    /// <summary>
    /// Option
    /// </summary>
    [JsonProperty("option")]
    public List<string> Option { get; set; } = [];

    /// <summary>
    /// Spot
    /// </summary>
    [JsonProperty("spot")]
    public List<string> Spot { get; set; } = [];
}
