namespace OKX.Api.Models.Public;

public class OkxOracle
{
    [JsonProperty("messages")]
    public List<string> Messages { get; set; }

    [JsonProperty("signatures")]
    public List<string> Signatures { get; set; }

    [JsonProperty("timestamp")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    [JsonProperty("prices")]
    public Dictionary<string, decimal> Prices { get; set; } = new Dictionary<string, decimal>();
}
