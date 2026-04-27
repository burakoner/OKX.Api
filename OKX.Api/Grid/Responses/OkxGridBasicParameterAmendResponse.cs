namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Basic Parameter Amend Response
/// </summary>
public record OkxGridBasicParameterAmendResponse : OkxRestApiErrorBase
{
    /// <summary>
    /// Algo Order Id
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Maximum top up amount
    /// </summary>
    [JsonProperty("maxTopupAmount"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? MaximumTopupAmount { get; set; }

    /// <summary>
    /// Required top up amount
    /// </summary>
    [JsonProperty("requiredTopupAmount"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? RequiredTopupAmount { get; set; }
}
