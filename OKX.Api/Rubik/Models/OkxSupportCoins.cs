namespace OKX.Api.Rubik.Models;

public class OkxSupportCoins
{
    [JsonProperty("contract")]
    public List<string> Contract { get; set; }

    [JsonProperty("option")]
    public List<string> Option { get; set; }

    [JsonProperty("spot")]
    public List<string> Spot { get; set; }
}
