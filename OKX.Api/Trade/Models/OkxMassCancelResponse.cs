namespace OKX.Api.Trade.Models;

public class OkxMassCancelResponse
{
    [JsonProperty("result")]
    public bool Result { get; set; }
}