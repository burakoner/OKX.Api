﻿namespace OKX.Api.Public;

/// <summary>
/// OKX Mark Price
/// </summary>
public record OkxPublicMarkPrice
{
    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Mark Price
    /// </summary>
    [JsonProperty("markPx")]
    public decimal MarkPrice { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
