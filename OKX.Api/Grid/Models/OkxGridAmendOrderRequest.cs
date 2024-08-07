﻿using OKX.Api.Grid.Converters;
using OKX.Api.Grid.Enums;

namespace OKX.Api.Grid.Models;

/// <summary>
/// OKX Grid Amend Order Request
/// </summary>
public class OkxGridAmendOrderRequest
{
    /// <summary>
    /// Algo order ID
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoOrderId { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Take profit trigger price
    /// </summary>
    [JsonProperty("tpTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Stop loss trigger price
    /// </summary>
    [JsonProperty("slTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? StopLossTriggerPrice { get; set; }

    /// <summary>
    /// Trigger parameters
    /// </summary>
    [JsonProperty("triggerParams", NullValueHandling = NullValueHandling.Ignore)]
    public List<OkxGridAmendTriggerParameters> TriggerParameters { get; set; }
}

/// <summary>
/// OKX Grid Amend Trigger Parameters
/// </summary>
public class OkxGridAmendTriggerParameters
{
    /// <summary>
    /// Trigger action
    /// </summary>
    [JsonProperty("triggerAction"), JsonConverter(typeof(OkxGridAlgoTriggerActionConverter))]
    public OkxGridAlgoTriggerAction TriggerAction { get; set; }

    /// <summary>
    /// Trigger strategy
    /// </summary>
    [JsonProperty("triggerStrategy"), JsonConverter(typeof(OkxGridAlgoTriggerStrategyConverter))]
    public OkxGridAlgoTriggerStrategy TriggerStrategy { get; set; }

    /// <summary>
    /// Trigger price
    /// </summary>
    [JsonProperty("triggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string TriggerPrice { get; set; }

    /// <summary>
    /// Spot algo stop type
    /// </summary>
    [JsonIgnore]
    public OkxGridSpotAlgoStopType? SpotAlgoStopType { get; set; }

    /// <summary>
    /// Contract algo stop type
    /// </summary>
    [JsonIgnore]
    public OkxGridContractAlgoStopType? ContractAlgoStopType { get; set; }

    /// <summary>
    /// Algo stop type
    /// </summary>
    [JsonProperty("stopType", NullValueHandling = NullValueHandling.Ignore)]
    public string AlgoStopType
    {
        get
        {
            if (SpotAlgoStopType is not null) return JsonConvert.SerializeObject(SpotAlgoStopType, new OkxGridSpotAlgoStopTypeConverter(false));
            if (ContractAlgoStopType is not null) return JsonConvert.SerializeObject(ContractAlgoStopType, new OkxGridContractAlgoStopTypeConverter(false));
            return null;
        }
    }

}
