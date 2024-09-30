namespace OKX.Api.Funding;

/// <summary>
/// OKX Convert Dust Assets Response
/// </summary>
public record OkxFundingConvertDustAssets
{
    /// <summary>
    /// Total quantity of OKB after conversion
    /// </summary>
    [JsonProperty("totalCnvAmt")]
    public decimal TotalOkbAmount { get; set; }

    /// <summary>
    /// Details of asset conversion
    /// </summary>
    [JsonProperty("details")]
    public List<OkxFundingConvertDustAssetsDetails> Details { get; set; } = [];
}
