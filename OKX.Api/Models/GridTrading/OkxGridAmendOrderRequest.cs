﻿namespace OKX.Api.Models.GridTrading;

public class OkxGridAmendOrderRequest
{
    [JsonProperty("algoId")]
    public long AlgoOrderId { get; set; }

    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    [JsonProperty("tpTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? TakeProfitTriggerPrice { get; set; }

    [JsonProperty("slTriggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? StopLossTriggerPrice { get; set; }

    [JsonProperty("triggerParams", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable< OkxGridAmendTriggerParameters> TriggerParameters { get; set; }
}

public class OkxGridAmendTriggerParameters
{
    [JsonProperty("triggerAction"), JsonConverter(typeof(GridAlgoTriggerActionConverter))]
    public OkxGridAlgoTriggerAction TriggerAction { get; set; }

    [JsonProperty("triggerStrategy"), JsonConverter(typeof(GridAlgoTriggerStrategyConverter))]
    public OkxGridAlgoTriggerStrategy TriggerStrategy { get; set; }

    [JsonProperty("triggerPx", NullValueHandling = NullValueHandling.Ignore)]
    public string TriggerPrice { get; set; }

    [JsonIgnore]
    public OkxGridSpotAlgoStopType? SpotAlgoStopType { get; set; }

    [JsonIgnore]
    public OkxGridContractAlgoStopType? ContractAlgoStopType { get; set; }

    [JsonProperty("stopType", NullValueHandling = NullValueHandling.Ignore)]
    public string AlgoStopType
    {
        get
        {
            if (SpotAlgoStopType != null) return JsonConvert.SerializeObject(SpotAlgoStopType, new GridSpotAlgoStopTypeConverter(false));
            if (ContractAlgoStopType != null) return JsonConvert.SerializeObject(ContractAlgoStopType, new GridContractAlgoStopTypeConverter(false));
            return null;
        }
    }

}
