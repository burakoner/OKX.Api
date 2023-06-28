namespace OKX.Api.Models.MarketData;

public class OkxOrderBook
{
    public string Instrument { get; set; }

    [JsonProperty("asks")]
    public IEnumerable<OkxOrderBookRow> Asks { get; set; } = new List<OkxOrderBookRow>();

    [JsonProperty("bids")]
    public IEnumerable<OkxOrderBookRow> Bids { get; set; } = new List<OkxOrderBookRow>();

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("action")]
    public string Action { get; set; }

    [JsonProperty("checksum")]
    public long? Checksum { get; set; }

    [JsonProperty("prevSeqId")]
    public long? PreviousSequenceId { get; set; }

    [JsonProperty("seqId")]
    public long? SequenceId { get; set; }
}
