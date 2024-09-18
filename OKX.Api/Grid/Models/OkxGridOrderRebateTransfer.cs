using OKX.Api.Grid.Converters;
using OKX.Api.Grid.Enums;

namespace OKX.Api.Grid.Models;

/// <summary>
/// Rebate transfer info
/// </summary>
public class OkxGridOrderRebateTransfer
{
    /// <summary>
    /// Rebate amount
    /// </summary>
    [JsonProperty("rebate")]
    public decimal Rebate { get; set; }

    /// <summary>
    /// Rebate currency
    /// </summary>
    [JsonProperty("rebateCcy")]
    public string RebateCurrency { get; set; }
}
