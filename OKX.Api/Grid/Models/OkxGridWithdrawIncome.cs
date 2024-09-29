namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Withdraw Income
/// </summary>
public class OkxGridWithdrawIncome
{
    /// <summary>
    /// Algo Order Id
    /// </summary>
    [JsonProperty("algoId")]
    public string AlgoOrderId { get; set; }

    /// <summary>
    /// Algo Client Order Id
    /// </summary>
    [JsonProperty("algoClOrdId")]
    public string AlgoClientOrderId { get; set; }

    /// <summary>
    /// Profit
    /// </summary>
    [JsonProperty("profit")]
    public decimal Profit { get; set; }
}