﻿namespace OKX.Api.Models.MarketData;

public class OkxTicker
{
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("last")]
    public decimal LastPrice { get; set; }

    [JsonProperty("lastSz")]
    public decimal LastSize { get; set; }

    [JsonProperty("askPx")]
    public decimal AskPrice { get; set; }

    [JsonProperty("askSz")]
    public decimal AskSize { get; set; }

    [JsonProperty("bidPx")]
    public decimal BidPrice { get; set; }

    [JsonProperty("bidSz")]
    public decimal BidSize { get; set; }

    [JsonProperty("open24h")]
    public decimal Open { get; set; }

    [JsonProperty("high24h")]
    public decimal High { get; set; }

    [JsonProperty("low24h")]
    public decimal Low { get; set; }

    /// <summary>
    /// Base Volume
    /// </summary>
    [JsonProperty("volCcy24h")]
    public decimal VolumeCurrency { get; set; }

    /// <summary>
    /// Quote Volume
    /// </summary>
    [JsonProperty("vol24h")]
    public decimal Volume { get; set; }

    [JsonProperty("sodUtc0")]
    public decimal OpenPriceUtc0 { get; set; }

    [JsonProperty("sodUtc8")]
    public decimal OpenPriceUtc8 { get; set; }

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
