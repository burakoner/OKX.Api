using OKX.Api.Public.Converters;
using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Models;

/// <summary>
/// OKX Insurance Fund
/// </summary>
public class OkxInsuranceFund
{
    /// <summary>
    /// Total
    /// </summary>
    [JsonProperty("total")]
    public decimal Total { get; set; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public List<OkxInsuranceFundDetail> Details { get; set; }
}

/// <summary>
/// OKX Insurance Fund Detail
/// </summary>
public class OkxInsuranceFundDetail
{
    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Balance
    /// </summary>
    [JsonProperty("balance")]
    public decimal Balance { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(OkxInsuranceTypeConverter))]
    public OkxInsuranceType Type { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}
