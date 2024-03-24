namespace OKX.Api.AlgoTrading.Models;

public class OkxAlgoOrderAmendResponse : OkxRestApiResponseModel
{
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; }

    [JsonProperty("algoId")]
    public long? AlgoOrderId { get; set; }

    [JsonProperty("reqId")]
    public string ClientRequestId { get; set; }

    [JsonProperty("sCode")]
    public string ErrorCode { get; set; }

    [JsonProperty("sMsg")]
    public string ErrorMessage { get; set; }
}
