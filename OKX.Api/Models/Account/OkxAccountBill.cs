namespace OKX.Api.Models.Account;

public class OkxAccountBill
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType? InstrumentType { get; set; }

    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OkxMarginMode? MarginMode { get; set; }

    [JsonProperty("billId")]
    public long? BillId { get; set; }

    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    [JsonProperty("bal")]
    public decimal? Balance { get; set; }

    [JsonProperty("balChg")]
    public decimal? BalanceChange { get; set; }

    [JsonProperty("sz")]
    public decimal? Quantity { get; set; }

    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    [JsonProperty("from"), JsonConverter(typeof(AccountConverter))]
    public OkxAccount? FromAccount { get; set; }

    [JsonProperty("to"), JsonConverter(typeof(AccountConverter))]
    public OkxAccount? ToAccount { get; set; }

    [JsonProperty("notes")]
    public string Notes { get; set; }
}
