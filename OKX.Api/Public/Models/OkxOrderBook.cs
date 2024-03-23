namespace OKX.Api.Public.Models;

public class OkxOrderBook
{
    public string Instrument { get; set; }

    [JsonProperty("asks")]
    public List<OkxOrderBookRow> Asks { get; set; } = [];

    [JsonProperty("bids")]
    public List<OkxOrderBookRow> Bids { get; set; } = [];

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

    [JsonProperty("action")]
    public string Action { get; set; }

    [JsonProperty("checksum")]
    public long? Checksum { get; set; }

    [JsonProperty("prevSeqId")]
    public long? PreviousSequenceId { get; set; }

    [JsonProperty("seqId")]
    public long? SequenceId { get; set; }
}
