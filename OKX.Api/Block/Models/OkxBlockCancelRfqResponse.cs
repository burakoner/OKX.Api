﻿namespace OKX.Api.Block.Models;

/// <summary>
/// OKX Block Trading Cancel RFQ Response
/// </summary>
public class OkxBlockCancelRfqResponse : OkxRestApiErrorResponse
{
    /// <summary>
    /// RFQ ID
    /// </summary>
    [JsonProperty("rfqId")]
    public long RfqId { get; set; }

    /// <summary>
    /// Client-supplied RFQ ID.
    /// </summary>
    [JsonProperty("clRfqId")]
    public string ClientRfqId { get; set; }
}