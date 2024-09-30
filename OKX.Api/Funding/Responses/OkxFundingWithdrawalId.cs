namespace OKX.Api.Funding;

/// <summary>
/// OKX Withdrawal Id
/// </summary>
internal record OkxFundingWithdrawalId
{
    /// <summary>
    /// Withdrawal Id
    /// </summary>
    [JsonProperty("wdId")]
    public long Data { get; set; }
}