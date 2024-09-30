namespace OKX.Api.Grid;

/// <summary>
/// Rebate transfer info
/// </summary>
public record OkxGridOrderRebateTransfer
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
    public string RebateCurrency { get; set; } = string.Empty;
}
