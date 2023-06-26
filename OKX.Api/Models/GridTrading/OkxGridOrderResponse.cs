namespace OKX.Api.Models.GridTrading;

public class OkxGridOrderResponse
{
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; }

    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; }

    [JsonProperty("ordId")]
    public string OrderId { get; set; }

    [JsonProperty("sCode")]
    public string ErrorCode { get; set; }

    [JsonProperty("sMsg")]
    public string ErrorMessage { get; set; }

    [JsonProperty("tag")]
    internal string Tag { get; set; }
}