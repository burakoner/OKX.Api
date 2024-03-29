﻿namespace OKX.Api.GridTrading.Models;

public class OkxGridWithdrawIncome
{
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; }

    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; }

    [JsonProperty("profit")]
    public decimal Profit { get; set; }
}