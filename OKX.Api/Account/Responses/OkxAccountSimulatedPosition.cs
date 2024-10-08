﻿namespace OKX.Api.Account;

/// <summary>
/// Okx Simulated Position Request
/// </summary>
public record OkxAccountSimulatedPosition
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Quantity of positions
    /// </summary>
    [JsonProperty("pos")]
    public decimal Quantity { get; set; }
}