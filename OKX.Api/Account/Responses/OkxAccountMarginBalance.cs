﻿namespace OKX.Api.Account;

/// <summary>
/// OkxMarginBalance
/// </summary>
public record OkxAccountMarginBalance
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Position side, long short
    /// </summary>
    [JsonProperty("posSide")]
    public OkxTradePositionSide PositionSide { get; set; }

    /// <summary>
    /// Amount to be increase or decrease
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// add: add margin, or transfer collaterals in (Quick Margin Mode)
    /// reduce: reduce margin, transfer collaterals out (Quick Margin Mode)
    /// </summary>
    [JsonProperty("type")]
    public OkxAccountMarginAddReduce Type { get; set; }

    /// <summary>
    /// Real leverage after the margin adjustment
    /// </summary>
    [JsonProperty("leverage")]
    public int Leverage { get; set; }

    /// <summary>
    /// Currency, only applicable to MARGIN（Manual transfers and Quick Margin Mode）
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;
}
