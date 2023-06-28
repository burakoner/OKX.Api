namespace OKX.Api.Models.GridTrading;

public class OkxGridAiParameter
{
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    [JsonProperty("algoOrdType"), JsonConverter(typeof(GridAlgoOrderTypeConverter))]
    public OkxGridAlgoOrderType AlgoOrderType { get; set; }

    [JsonProperty("duration"), JsonConverter(typeof(GridBackTestingDurationConverter))]
    public OkxGridBackTestingDuration Duration { get; set; }

    [JsonProperty("maxPx")]
    public decimal? MaximumPrice { get; set; }

    [JsonProperty("minPx")]
    public decimal? MinimumPrice { get; set; }

    [JsonProperty("gridNum")]
    public decimal? Quantity { get; set; }

    [JsonProperty("perMaxProfitRate")]
    public decimal? MaximumProfitRatePerGrid { get; set; }

    [JsonProperty("perMinProfitRate")]
    public decimal? MinimumProfitRatePerGrid { get; set; }

    [JsonProperty("annualizedRate")]
    public decimal? AnnualizedRate { get; set; }

    [JsonProperty("minInvestment")]
    public decimal? MinimumInvestment { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("runType"), JsonConverter(typeof(GridRunTypeConverter))]
    public OkxGridRunType RunType { get; set; }
    
    [JsonProperty("direction"), JsonConverter(typeof(GridContractDirectionConverter))]
    public OkxGridContractDirection Direction { get; set; }

    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }
}