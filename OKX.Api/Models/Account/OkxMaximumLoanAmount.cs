namespace OKX.Api.Models.Account;

public class OkxMaximumLoanAmount
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OkxMarginMode? MarginMode { get; set; }

    [JsonProperty("mgnCcy")]
    public string MarginCurrency { get; set; }

    [JsonProperty("maxLoan")]
    public decimal? MaximumLoan { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
    public OkxOrderSide? OrderSide { get; set; }
}
