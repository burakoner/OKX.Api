﻿namespace OKX.Api.Account;

/// <summary>
/// Okx MMP Configuration
/// </summary>
public record OkxAccountMmpConfigurationData
{
    /// <summary>
    /// Instrument Family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = string.Empty;

    /// <summary>
    /// MMP Frozen
    /// </summary>
    [JsonProperty("mmpFrozen")]
    public bool MmpFrozen { get; set; }

    /// <summary>
    /// MMP Frozen Until
    /// </summary>
    [JsonProperty("mmpFrozenUntil")]
    public string MmpFrozenUntil { get; set; } = string.Empty;

    /// <summary>
    /// Time Interval
    /// </summary>
    [JsonProperty("timeInterval")]
    public int TimeInterval { get; set; }

    /// <summary>
    /// Frozen Interval
    /// </summary>
    [JsonProperty("frozenInterval")]
    public int FrozenInterval { get; set; }

    /// <summary>
    /// Quantity Limit
    /// </summary>
    [JsonProperty("qtyLimit")]
    public int QuantityLimit { get; set; }
}