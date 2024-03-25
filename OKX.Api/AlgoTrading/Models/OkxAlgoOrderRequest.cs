namespace OKX.Api.AlgoTrading.Models;

public class OkxAlgoOrderRequest
{
    [JsonProperty("algoId")]
    public long AlgoOrderId { get; set; }

    [JsonProperty("instId")]
    public string InstrumentId { get; set; }
}
