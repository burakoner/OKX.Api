﻿namespace OKX.Api.Block;

/// <summary>
/// OKX Block Counterparty
/// </summary>
public record OkxBlockCounterparty
{
    /// <summary>
    /// The long formative username of trader or entity on the platform.
    /// </summary>
    [JsonProperty("traderName")]
    public string TraderName { get; set; } = string.Empty;

    /// <summary>
    /// A unique identifier of maker which will be publicly visible on the platform. All RFQ and Quote endpoints will use this as the unique counterparty identifier.
    /// </summary>
    [JsonProperty("traderCode")]
    public string TraderCode { get; set; } = string.Empty;

    /// <summary>
    /// The counterparty type. LP refers to API connected auto market makers.
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;
}
