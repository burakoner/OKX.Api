namespace OKX.Api.Funding;

/// <summary>
/// OKX Convert Dust Assets Response
/// </summary>
public class OkxFundingConvertDustAssetsResponse
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

/// <summary>
/// OKX Convert Dust Assets Details
/// </summary>
public class OkxFundingConvertDustAssetsDetails
{
    /// <summary>
    /// Currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }
    
    /// <summary>
    /// Quantity of currency assets before conversion
    /// </summary>
    [JsonProperty("amt")]
    public decimal CurrencyAmount { get; set; }

    /// <summary>
    /// Quantity of OKB after conversion
    /// </summary>
    [JsonProperty("cnvAmt")]
    public decimal OkbAmount { get; set; }

    /// <summary>
    /// Fee for conversion, unit in OKB
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }
}