namespace OKX.Api.Grid.Models;

/// <summary>
/// OKX Grid Investment Add Response
/// </summary>
public class OkxGridInvestmentAddResponse
{
    /// <summary>
    /// Algo Order Id
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; }
}