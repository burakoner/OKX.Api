﻿using OKX.Api.Public.Converters;
using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Models;

public class OkxInsuranceFund
{
    [JsonProperty("total")]
    public decimal Total { get; set; }

    [JsonProperty("details")]
    public List<OkxInsuranceFundDetail> Details { get; set; }
}

public class OkxInsuranceFundDetail
{
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(OkxInsuranceTypeConverter))]
    public OkxInsuranceType Type { get; set; }

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
