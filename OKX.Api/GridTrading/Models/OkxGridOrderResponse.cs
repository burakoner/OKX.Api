namespace OKX.Api.GridTrading.Models;

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

    /// <summary>
    /// Order tag. Used for Broker Id
    /// </summary>
    [JsonProperty("tag")]
    internal string Tag { get; set; }
}