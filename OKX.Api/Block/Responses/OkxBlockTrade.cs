﻿namespace OKX.Api.Block;

/// <summary>
/// OKX Block Execution
/// </summary>
public class OkxBlockTrade
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
    /// The unique identifier of the RFQ generated by system.
    /// </summary>
    [JsonProperty("rfqId")]
    public long RfqId { get; set; }

    /// <summary>
    /// Client-supplied RFQ ID. This attribute is treated as client sensitive information. It will not be exposed to the Maker, only return empty string.
    /// </summary>
    [JsonProperty("clRfqId")]
    public string ClientOrderId { get; set; } = "";

    /// <summary>
    /// Quote ID.
    /// </summary>
    [JsonProperty("quoteId")]
    public long QuoteId { get; set; }

    /// <summary>
    /// Client-supplied Quote ID. This attribute is treated as client sensitive information. It will not be exposed to the Taker, only return empty string.
    /// </summary>
    [JsonProperty("clQuoteId")]
    public string ClientQuoteId { get; set; } = "";

    /// <summary>
    /// Block trade ID.
    /// </summary>
    [JsonProperty("blockTdId")]
    public long BlockTradeId { get; set; }

    /// <summary>
    /// A unique identifier of the taker. Empty if the anonymous parameter of the RFQ is set to be true.
    /// </summary>
    [JsonProperty("tTraderCode")]
    public string TakerTraderCode { get; set; } = "";

    /// <summary>
    /// A unique identifier of the maker. Empty if the anonymous parameter of the Quote is set to be true.
    /// </summary>
    [JsonProperty("mTraderCode")]
    public string MakerTraderCode { get; set; } = "";

    /// <summary>
    /// Legs of trade
    /// </summary>
    [JsonProperty("legs")]
    public List<OkxBlockLegTrade> Legs { get; set; } = [];
}