﻿namespace OKX.Api.SignalBot;

/// <summary>
/// OKX Signal Exit Paramaters
/// </summary>
public record OkxSignalBotExitParamaters
{
    /// <summary>
    /// Type of set the take-profit and stop-loss trigger price
    /// </summary>
    [JsonProperty("tpSlType")]
    public OkxSignalBotTriggerPrice TakeProfitStopLossTriggerPrice { get; set; }

    /// <summary>
    /// Take-profit percentage
    /// </summary>
    [JsonProperty("tpPct")]
    public string TakeProfitPercentage { get; set; } = string.Empty;

    /// <summary>
    /// Stop-loss percentage
    /// </summary>
    [JsonProperty("slPct")]
    public string StopLossPercentage { get; set; } = string.Empty;
}