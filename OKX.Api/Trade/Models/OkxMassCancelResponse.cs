﻿namespace OKX.Api.Trade.Models;

/// <summary>
/// OKX Mass Cancel Response
/// </summary>
public class OkxMassCancelResponse
{
    /// <summary>
    /// Result
    /// </summary>
    [JsonProperty("result")]
    public bool Result { get; set; }
}