﻿namespace OKX.Api.Public.Models;

/// <summary>
/// OKX Oracle
/// </summary>
public class OkxOracle
{
    /// <summary>
    /// Messages
    /// </summary>
    [JsonProperty("messages")]
    public List<string> Messages { get; set; }

    /// <summary>
    /// Signatures
    /// </summary>
    [JsonProperty("signatures")]
    public List<string> Signatures { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("timestamp")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Prices
    /// </summary>
    [JsonProperty("prices")]
    public Dictionary<string, decimal> Prices { get; set; } = new Dictionary<string, decimal>();
}
