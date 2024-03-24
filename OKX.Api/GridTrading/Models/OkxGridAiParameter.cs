using OKX.Api.GridTrading.Converters;
using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Models;

public class OkxGridAiParameter
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("algoOrdType"), JsonConverter(typeof(OkxGridAlgoOrderTypeConverter))]
    public OkxGridAlgoOrderType AlgoOrderType { get; set; }

    [JsonProperty("duration"), JsonConverter(typeof(OkxGridBackTestingDurationConverter))]
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

    [JsonProperty("runType"), JsonConverter(typeof(OkxGridRunTypeConverter))]
    public OkxGridRunType RunType { get; set; }

    [JsonProperty("direction"), JsonConverter(typeof(OkxGridContractDirectionConverter))]
    public OkxGridContractDirection Direction { get; set; }

    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }
}