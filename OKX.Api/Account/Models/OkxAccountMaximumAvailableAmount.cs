﻿namespace OKX.Api.Account.Models;

/// <summary>
/// OkxMaximumAvailableAmount
/// </summary>
public class OkxAccountMaximumAvailableAmount
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Amount available to buy
    /// </summary>
    [JsonProperty("availBuy")]
    public decimal AvailableBuy { get; set; }

    /// <summary>
    /// Amount available to sell
    /// </summary>
    [JsonProperty("availSell")]
    public decimal AvailableSell { get; set; }
}
