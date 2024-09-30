﻿namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Margin Order Response
/// </summary>
public class OkxGridMarginOrderResponse
{
    /// <summary>
    /// Algo Order Id
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; } = "";

    /// <summary>
    /// Algo Client Order Id
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; } = "";
}