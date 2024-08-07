﻿using OKX.Api.Grid.Converters;
using OKX.Api.Grid.Enums;

namespace OKX.Api.Grid.Models;

/// <summary>
/// OKX Grid AI Parameter
/// </summary>
public class OkxGridAiParameter
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Algo order type
    /// </summary>
    [JsonProperty("algoOrdType"), JsonConverter(typeof(OkxGridAlgoOrderTypeConverter))]
    public OkxGridAlgoOrderType AlgoOrderType { get; set; }

    /// <summary>
    /// Duration
    /// </summary>
    [JsonProperty("duration"), JsonConverter(typeof(OkxGridBackTestingDurationConverter))]
    public OkxGridBackTestingDuration Duration { get; set; }

    /// <summary>
    /// Maximum price
    /// </summary>
    [JsonProperty("maxPx")]
    public decimal? MaximumPrice { get; set; }

    /// <summary>
    /// Minimum price
    /// </summary>
    [JsonProperty("minPx")]
    public decimal? MinimumPrice { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("gridNum")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Maximum profit rate per grid
    /// </summary>
    [JsonProperty("perMaxProfitRate")]
    public decimal? MaximumProfitRatePerGrid { get; set; }

    /// <summary>
    /// Minimum profit rate per grid
    /// </summary>
    [JsonProperty("perMinProfitRate")]
    public decimal? MinimumProfitRatePerGrid { get; set; }

    /// <summary>
    /// Annualized rate
    /// </summary>
    [JsonProperty("annualizedRate")]
    public decimal? AnnualizedRate { get; set; }

    /// <summary>
    /// Minimum investment
    /// </summary>
    [JsonProperty("minInvestment")]
    public decimal? MinimumInvestment { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Run type
    /// </summary>
    [JsonProperty("runType"), JsonConverter(typeof(OkxGridRunTypeConverter))]
    public OkxGridRunType RunType { get; set; }

    /// <summary>
    /// Direction
    /// </summary>
    [JsonProperty("direction"), JsonConverter(typeof(OkxGridContractDirectionConverter))]
    public OkxGridContractDirection Direction { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }
}