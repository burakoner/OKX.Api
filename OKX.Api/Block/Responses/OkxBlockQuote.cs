﻿namespace OKX.Api.Block;

/// <summary>
/// OKX Block Quote
/// </summary>
public record OkxBlockQuote
{
    /// <summary>
    /// The timestamp the RFQ was created. Unix timestamp format in milliseconds.
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// The timestamp the RFQ was created.
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// The timestamp the RFQ was last updated. Unix timestamp format in milliseconds.
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// The timestamp the RFQ was last updated.
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// The status of the RFQ.
    /// Valid values can be active canceled pending_fill filled expired traded_away failed.
    /// traded_away only applies to Maker
    /// </summary>
    [JsonProperty("state")]
    public OkxBlockState State { get; set; }

    /// <summary>
    /// Reasons of state. Valid values can be mmp_canceled.
    /// </summary>
    [JsonProperty("reason")]
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// The timestamp the RFQ expires. Unix timestamp format in milliseconds.
    /// If all legs are options, the RFQ will expire after 10 minutes; otherwise, the RFQ will expire after 2 minutes.
    /// </summary>
    [JsonProperty("validUntil")]
    public long? ValidUntilTimestamp { get; set; }

    /// <summary>
    /// The timestamp the RFQ expires.
    /// If all legs are options, the RFQ will expire after 10 minutes; otherwise, the RFQ will expire after 2 minutes.
    /// </summary>
    [JsonIgnore]
    public DateTime? ValidUntilTime => ValidUntilTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// RFQ Id
    /// </summary>
    [JsonProperty("rfqId")]
    public string RfqId { get; set; } = string.Empty;

    /// <summary>
    /// Client-supplied RFQ ID. This attribute is treated as client sensitive information. It will not be exposed to the Maker, only return empty string.
    /// </summary>
    [JsonProperty("clRfqId")]
    public string ClientRfqId { get; set; } = string.Empty;

    /// <summary>
    /// Quote Id
    /// </summary>
    [JsonProperty("quoteId")]
    public string QuoteId { get; set; } = string.Empty;

    /// <summary>
    /// Client Quote Id
    /// </summary>
    [JsonProperty("clQuoteId")]
    public string ClientQuoteId { get; set; } = string.Empty;

    /// <summary>
    /// A unique identifier of maker. Empty If the anonymous parameter of the Quote is set to be true.
    /// </summary>
    [JsonProperty("traderCode")]
    public string TraderCode { get; set; } = string.Empty;

    /// <summary>
    /// Top level direction of Quote. Its value can be buy or sell.
    /// </summary>
    [JsonProperty("quoteSide")]
    public OkxTradeOrderSide QuoteSide { get; set; }

    /// <summary>
    /// The legs of the Quote.
    /// </summary>
    [JsonProperty("legs")]
    public List<OkxBlockLegQuote> Legs { get; set; } = [];
}
