﻿namespace OKX.Api.Models.AlgoTrading;

public class OkxAlgoOrderResponse : OkxRestApiResponseModel
{
    [JsonProperty("algoId")]
    public long? AlgoOrderId { get; set; }

    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    [JsonProperty("algoClOrdId")]
    public string ClientAlgoOrderId { get; set; }
}
