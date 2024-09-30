namespace OKX.Api.Base;

/// <summary>
/// OKX Order Book Update
/// </summary>
public record OkxSocketOrderBookUpdate
{
    /// <summary>
    /// Action
    /// </summary>
    [JsonProperty("action")]
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Arguments
    /// </summary>
    [JsonProperty("arg")]
    public OkxSocketUpdateArguments? Arguments { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    [JsonProperty("data")]
    public List<OkxPublicOrderBookStream> Data { get; set; } = [];
}