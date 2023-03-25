namespace OKX.Api.Models.Market;

public class OkxIndexComponents
{
    [JsonProperty("last")]
    public decimal LastPrice { get; set; }

    [JsonProperty("index")]
    public string Index { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("components")]
    public IEnumerable<OkxIndexComponent> Components { get; set; }
}

public class OkxIndexComponent
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("symPx")]
    public decimal Price { get; set; }

    [JsonProperty("wgt")]
    public decimal Weight { get; set; }

    [JsonProperty("cnvPx")]
    public decimal ConvertPrice { get; set; }

    [JsonProperty("exch")]
    public string Exchange { get; set; }
}
