﻿namespace OKX.Api.Block.Models;

/// <summary>
/// OKX Block Trading Cancel Quote Response
/// </summary>
public class OkxBlockCancelQuoteResponse : OkxRestApiErrorResponse
{
    /// <summary>
    /// Quote ID
    /// </summary>
    [JsonProperty("quoteId")]
    public long QuoteId { get; set; }

    /// <summary>
    /// Client-supplied Quote ID.
    /// </summary>
    [JsonProperty("clQuoteId")]
    public string ClientQuoteId { get; set; }
}