namespace OKX.Api.Public.Models;

public class OkxIndexComponents
{
    [JsonProperty("components")]
    public List<OkxIndexComponent> Components { get; set; }

    [JsonProperty("index")]
    public string Index { get; set; }

    [JsonProperty("last")]
    public decimal LastPrice { get; set; }

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}

public class OkxIndexComponent
{
    [JsonProperty("exch")]
    public string Exchange { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("symPx")]
    public decimal Price { get; set; }

    [JsonProperty("wgt")]
    public decimal Weight { get; set; }

    [JsonProperty("cnvPx")]
    public decimal ConvertPrice { get; set; }
}
