﻿namespace OKX.Api.Account.Models;

/// <summary>
/// Okx Simulated Position Request
/// </summary>
public class OkxAccountSimulatedPosition
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Quantity of positions
    /// </summary>
    [JsonProperty("pos")]
    public decimal Quantity { get; set; }
}