namespace OKX.Api.Models.Account;

public class OkxAccountBill
{
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType? InstrumentType { get; set; }

    [JsonProperty("billId")]
    public long? BillId { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(AccountBillTypeConverter))]
    public OkxAccountBillType? BillType { get; set; }

    [JsonProperty("subType"), JsonConverter(typeof(AccountBillSubTypeConverter))]
    public OkxAccountBillSubType? BillSubType { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("balChg")]
    public decimal? AccountBalanceChange { get; set; }
    
    [JsonProperty("posBalChg")]
    public decimal? PositionBalanceChange { get; set; }

    [JsonProperty("bal")]
    public decimal? AccountBalance { get; set; }

    [JsonProperty("posBal")]
    public decimal? PositionBalance { get; set; }

    [JsonProperty("sz")]
    public decimal? Quantity { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("pnl")]
    public decimal? ProfitNLoss { get; set; }

    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OkxMarginMode? MarginMode { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    [JsonProperty("execType"), JsonConverter(typeof(TradeRoleConverter))]
    public OkxTradeRole? TradeRole { get; set; }

    [JsonProperty("from"), JsonConverter(typeof(AccountConverter))]
    public OkxAccount? FromAccount { get; set; }

    [JsonProperty("to"), JsonConverter(typeof(AccountConverter))]
    public OkxAccount? ToAccount { get; set; }

    [JsonProperty("notes")]
    public string Notes { get; set; }
}
