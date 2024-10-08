﻿namespace OKX.Api.CopyTrading;

/// <summary>
/// OkxProfitSharingDetails
/// </summary>
public record OkxCopyTradingProfitSharingDetails
{
    /// <summary>
    /// The currency of profit sharing.
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Profit sharing amount. Default is 0.
    /// </summary>
    [JsonProperty("profitSharingAmt")]
    public decimal ProfitSharingAmount { get; set; }

    /// <summary>
    /// Nickname of copy trader.
    /// </summary>
    [JsonProperty("nickName")]
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    /// Profit sharing ID.
    /// </summary>
    [JsonProperty("profitSharingId")]
    public long ProfitSharingId { get; set; }
    
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Portrait link
    /// </summary>
    [JsonProperty("portLink")]
    public string PortraitLink { get; set; } = string.Empty;

    /// <summary>
    /// Profit sharing time.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Profit sharing time.
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}
