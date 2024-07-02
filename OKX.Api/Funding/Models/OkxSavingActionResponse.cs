using OKX.Api.Funding.Converters;
using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Models;

/// <summary>
/// OKX Saving Action Response
/// </summary>
public class OkxSavingActionResponse
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Purchase Rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal? PurchaseRate { get; set; }

    /// <summary>
    /// Side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxSavingActionSideConverter))]
    public OkxSavingActionSide Side { get; set; }
}