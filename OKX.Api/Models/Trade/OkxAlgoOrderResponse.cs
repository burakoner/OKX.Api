namespace OKX.Api.Models.Trade;

public class OkxAlgoOrderResponse: OkxRestApiResponseModel
{
    [JsonProperty("algoId")]
    public long? AlgoOrderId { get; set; }
}
